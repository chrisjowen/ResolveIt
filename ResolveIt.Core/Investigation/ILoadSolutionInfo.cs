using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public interface ILoadSolutionInfo
    {
        ISolutionInfo FromFile(string filePath);
    }
}