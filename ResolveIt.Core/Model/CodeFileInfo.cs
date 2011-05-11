using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ResolveIt.Core.Model
{
    public class CodeFileInfo : ICodeFileInfo
    {
        private readonly IList<IDeclerationInfo> declerations = new List<IDeclerationInfo>();
        public string Name { get; private set; }
        public string Path { get; private set; }
        public ProjectInfo Project { get; set; }

        public ISolutionInfo Solution
        {
            get { return Project.Solution; }
        }

        public IList<IDeclerationInfo> Declerations
        {
            get { return new ReadOnlyCollection<IDeclerationInfo>(declerations); }
        }

        public FileInfo AsFile()
        {
            return new FileInfo(FullPath);
        }

        public string FullPath
        {
            get { return string.Format(@"{0}\{1}", Path, Name); }
        }

        public string GetContent()
        {
            return File.ReadAllText(FullPath);
        }

        public CodeFileInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public void AddDecleration(IDeclerationInfo decleration)
        {
            declerations.Add(decleration);
        }
    }
}