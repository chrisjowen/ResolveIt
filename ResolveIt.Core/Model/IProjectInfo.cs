using System.Collections.Generic;

namespace ResolveIt.Core.Model
{
    public interface IProjectInfo
    {
        string Name { get; }
        string Path { get; }
        ISolutionInfo Solution { get; }
        IList<ICodeFileInfo> CodeFiles { get; }
        IList<IReferenceInfo> References { get; }
        void AddCodeFile(ICodeFileInfo codeFile);
        void AddReference(IReferenceInfo reference);
    }
}