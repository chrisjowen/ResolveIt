namespace WellItCouldWork.BuildCreation
{
    public interface ICompileBuildFiles
    {
        CompilationResult Compile(BuildFiles buildFiles);
    }
}