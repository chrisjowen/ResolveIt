using System.IO;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class CodeFileLoader : ILoadCodeFileInfo
    {
        private readonly IExamineSourceCodeForDeclerations declerationInfoLoader;

        public CodeFileLoader(IExamineSourceCodeForDeclerations declerationInfoLoader)
        {
            this.declerationInfoLoader = declerationInfoLoader;
        }

        public ICodeFileInfo FromFile(FileInfo file)
        {
            var codeFile =  new CodeFileInfo(file.Name, file.DirectoryName);
            var declerations = declerationInfoLoader.ExamineSource(codeFile);

            foreach (var decleration in declerations)
                codeFile.AddDecleration(decleration);  

            return codeFile;
        }
    }
}