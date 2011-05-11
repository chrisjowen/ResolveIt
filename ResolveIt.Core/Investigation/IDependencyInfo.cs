using System;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public interface IDependencyInfo
    {
        string Name { get;  }
        ICodeFileInfo CodeFileInfo { get; set; }
        bool IsExternal { get; }
    }

    public class DependencyInfo : IDependencyInfo
    {
        public string Name { get; private set; }
        public ICodeFileInfo CodeFileInfo { get; set; }

        public bool IsExternal
        {
            get { return CodeFileInfo == null; }
        }

        public DependencyInfo(string name)
        {
            Name = name;
        }
    }
}