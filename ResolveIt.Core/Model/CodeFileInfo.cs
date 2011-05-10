using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ResolveIt.Core.Model
{
    public class CodeFileInfo : ICodeFileInfo
    {
        private readonly IList<IDependencyInfo> dependencies = new List<IDependencyInfo>();
        public string Name { get; private set; }
        public string Path { get; private set; }
        public ProjectInfo Project { get; set; }

        public IList<IDependencyInfo> Dependencies
        {
            get { return new ReadOnlyCollection<IDependencyInfo>(dependencies); }
        }

        public FileInfo AsFile()
        {
            return new FileInfo(FullPath);
        }

        protected string FullPath
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

        public void AddDependency(IDependencyInfo dependency)
        {
            dependencies.Add(dependency);
        }
    }
}