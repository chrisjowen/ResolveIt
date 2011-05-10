namespace ResolveIt.Core.Model
{
    public interface IDependencyInfo
    {
        string Name { get; }
        ICodeFileInfo CodeFileInfo { get; }
    }
}