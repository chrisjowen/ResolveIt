using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResolveIt.Core.Model
{
    public class ProjectInfo : IProjectInfo
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public ISolutionInfo Solution { get; set; }
        private readonly IList<IReferenceInfo> references = new List<IReferenceInfo>();
        private readonly IList<ICodeFileInfo> codeFiles = new List<ICodeFileInfo>();

        public IList<IReferenceInfo> References
        {
            get { return new ReadOnlyCollection<IReferenceInfo>(references); }
        }
        public IList<ICodeFileInfo> CodeFiles
        {
            get { return new ReadOnlyCollection<ICodeFileInfo>(codeFiles); }
        }

        public void AddCodeFile(ICodeFileInfo codeFile)
        {
            codeFiles.Add(codeFile);
        }

        public void AddReference(IReferenceInfo reference)
        {
            references.Add(reference);
        }

        public ProjectInfo(string name, string path, ISolutionInfo solution)
        {
            Name = name;
            Path = path;
            Solution = solution;
            codeFiles = new List<ICodeFileInfo>();
            references = new List<IReferenceInfo>();
        }

        public ProjectInfo(string name, string path)
            : this(name, path, null)
        {
        }


    }
}
