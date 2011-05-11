namespace ResolveIt.Core.Model
{
    public interface IDeclerationInfo
    {
        string Name { get; }
        ICodeFileInfo CodeFileInfo { get; }
    }
}