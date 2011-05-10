using System;

namespace ResolveIt.Core.Model
{
    public class ReferenceInfo : IReferenceInfo
    {
        public string Path { get; private set; }
        public string Name { get; private set; }

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
    }
}