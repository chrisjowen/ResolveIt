using System;

namespace ResolveIt.Core.Model
{
    public class ReferenceInfo : IReferenceInfo
    {
        public string Path { get; private set; }
        public string Name { get; private set; }

        public bool IsExternal
        {
            get { return !String.IsNullOrEmpty(Path); }
        }

        public string FullPath
        {
            get { return string.IsNullOrEmpty(Path) ? Name  + ".dll" : string.Format("{0}/{1}", Path, Name); }
        }

        public ReferenceInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public ReferenceInfo(string name)
          :  this(name, string.Empty)
        {
        }
    }

    public interface IReferenceInfo
    {
        string Name { get; }
        bool IsExternal { get; }
        string FullPath { get;  }
    }
}