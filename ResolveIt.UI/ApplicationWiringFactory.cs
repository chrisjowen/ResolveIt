using System;
using ResolveIt.Core.BuildCreation;
using ResolveIt.Core.Investigation;

namespace ResolveIt.UI
{
    public class ApplicationWiringFactory
    {
        public ILoadSolutionInfo GetSolutionLoader()
        {
            IExamineSourceCodeForDeclerations declerationExaminer = new NRefactoryDeclerationExaminer();
            ILoadCodeFileInfo codeLoader = new CodeFileLoader(declerationExaminer);
            ILoadProjectInfo projectLoader = new ProjectLoader(codeLoader);
            return new SolutionLoader(projectLoader);
        }

        public IExamineSourceCodeForDependencies GetDependencyExaminer()
        {
            return new NRefactoryDependencyExaminer();
        }

        public ICompileBuildFiles GetBuildFileCompiler()
        {
            return new BuildFilesCompiler();
        }
    }
}
