using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using ResolveIt.Core.Exceptions;
using ResolveIt.Core.Model;
using ResolveIt.Core.SyntaxHelpers;

namespace ResolveIt.Core.Investigation
{
    public class ProjectLoader : ILoadProjectInfo
    {
        private readonly ILoadCodeFileInfo codeFileLoader;
        private ProjectInfo projectInfo;

        public ProjectLoader(ILoadCodeFileInfo codeFileLoader)
        {
            this.codeFileLoader = codeFileLoader;
        }

        public IProjectInfo FromFile(FileInfo file)
        {
            projectInfo = new ProjectInfo(file.Name, file.DirectoryName);

            var doc = XDocument.Load(file.OpenRead());
            var codeFiles = ResolveCodeFiles(doc);
            foreach (var codeFile in codeFiles)
                projectInfo.AddCodeFile(codeFile);

            var references = ResolveReferences(doc);
            foreach (var reference in references)
                projectInfo.AddReference(reference);

            return projectInfo;
        }

        private IEnumerable<IReferenceInfo> ResolveReferences(XDocument doc)
        {
            return doc.Document.Descendants()
              .Where(el => el.Name.LocalName == "Reference")
              .Select(ReferenceFor);
        }
        private IReferenceInfo ReferenceFor(XElement element)
        {

            var hintPath = element.Descendants()
                .Where(d => d.Name.LocalName == "HintPath")
                .Select(h => h.Value)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(hintPath))
            {
                var fileInfo = new FileInfo(projectInfo.Path + "\\" + hintPath);
                return new ReferenceInfo(fileInfo.Name, fileInfo.DirectoryName);
            }
            return new ReferenceInfo(AttributeFor(element, "Include"));
        }

        private IEnumerable<ICodeFileInfo> ResolveCodeFiles(XDocument doc)
        {
            return doc.Document.Descendants()
                .Where(decendent => decendent.Name.LocalName == "Compile")
                .Select(CodeFileFor);
        }
        private ICodeFileInfo CodeFileFor(XElement el)
        {
            var fileName = string.Format("{0}//{1}", projectInfo.Path, AttributeFor(el, "Include"));
            var file = new FileInfo(fileName);
            var codeFile =  codeFileLoader.FromFile(file);
            codeFile.Project = projectInfo;
            return codeFile;
        }

        private string AttributeFor(XElement el, string attribute)
        {
            if (!el.HasAttribute(attribute))
                throw new InvalidProjectFileComplaint(projectInfo);
            return el.Attribute(attribute).Value;
        }
    }
}