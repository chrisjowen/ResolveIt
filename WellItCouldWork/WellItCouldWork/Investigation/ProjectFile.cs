using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WellItCouldWork.Investigation.Exceptions;
using WellItCouldWork.SyntaxHelpers;

namespace WellItCouldWork.Investigation
{
    public class ProjectFile : IProjectFile
    {
        public string ProjectLocation { get; private set; }
        public string ProjectName { get; private set; }
        public IList<Reference> References { get; private set; }
        public IList<Class> Classes { get; private set; }

        private ProjectFile(string projectName, string projectLoacation, IEnumerable<XElement> docElements)
        {
            ProjectLocation = projectLoacation;
            ProjectName = projectName;
            References = ReferencesFrom(docElements);
            Classes = ClassesFrom(docElements);
        }

        public static ProjectFile Load(string fileName)
        {
            var projectFile = new FileInfo(fileName);
            var projectDocument = XDocument.Load(projectFile.OpenRead());
            var root = projectDocument.Document;
            return new ProjectFile(projectFile.Name,  projectFile.DirectoryName, root.Descendants());
        }


        private IList<Reference> ReferencesFrom(IEnumerable<XElement> elements)
        {
            return elements
                .Where(el => el.Name.LocalName == "Reference")
                .Select(ReferenceFor).ToList();
        }

        private List<Class> ClassesFrom(IEnumerable<XElement> elements)
        {
            return elements.Where(decendent => decendent.Name.LocalName == "Compile")
                .Select(el => string.Format("{0}//{1}", ProjectLocation, AttributeFor(el, "Include")))
                .Select(Class.FromPath).ToList();
        }

        private Reference ReferenceFor(XElement element)
        {

            var hintPath = element.Descendants()
                .Where(d => d.Name.LocalName == "HintPath")
                .Select(h => h.Value)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(hintPath))
            {
                var fileInfo = new FileInfo(ProjectLocation + "\\" + hintPath);
                return new Reference(fileInfo.Directory.FullName,  fileInfo.Name);
            }
            return new Reference(AttributeFor(element, "Include"));
        }

        private string AttributeFor(XElement el, string attribute)
        {
            if (!el.HasAttribute(attribute))
                throw new InvalidProjectFileComplaint(this);
            return el.Attribute(attribute).Value;
        }
    }


}