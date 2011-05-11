namespace ResolveIt.Core.BuildCreation
{
    public interface ICompileBuildFiles
    {
        CompilationResult Compile(BuildCodeFiles buildFiles);
    }
}