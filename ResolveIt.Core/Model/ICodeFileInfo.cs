using System.Collections.Generic;
using System.IO;

namespace ResolveIt.Core.Model
{
    public interface ICodeFileInfo
    {
        string Name { get; }
        string Path { get; }
        string FullPath { get; }
        ProjectInfo Project { get; set; }
        ISolutionInfo Solution { get; }
        IList<IDeclerationInfo> Declerations { get; }
        FileInfo AsFile();
        string  GetContent();
    }
}