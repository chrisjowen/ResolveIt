using System.IO;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class CodeFileLoader : ILoadCodeFileInfo
    {
        private readonly IExamineSourceCodeForDependencies dependencyInfoLoader;

        public CodeFileLoader(IExamineSourceCodeForDependencies dependencyInfoLoader)
        {
            this.dependencyInfoLoader = dependencyInfoLoader;
        }

        public ICodeFileInfo FromFile(FileInfo file)
        {
            var codeFile =  new CodeFileInfo(file.Name, file.DirectoryName);

            var dependencies = dependencyInfoLoader.ExamineSource(codeFile);

            foreach (var dependencyInfo in dependencies)
                codeFile.AddDependency(dependencyInfo);  

            return codeFile;
        }
    }
}