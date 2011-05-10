using System.Collections.Generic;
using System.IO;

namespace ResolveIt.Core.Model
{
    public interface ICodeFileInfo
    {
        string Name { get; }
        string Path { get; }
        ProjectInfo Project { get; set; }
        IList<IDependencyInfo> Dependencies { get; }
        FileInfo AsFile();
        string  GetContent();
    }
}