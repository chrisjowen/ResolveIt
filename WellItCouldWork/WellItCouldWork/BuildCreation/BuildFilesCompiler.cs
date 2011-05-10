using System.CodeDom.Compiler;
using System.Linq;
using Microsoft.CSharp;

namespace WellItCouldWork.BuildCreation
{
    public class BuildFilesCompiler : ICompileBuildFiles
    {
        public CompilationResult Compile(BuildFiles buildFiles)
        {
            using(var codeDomProvider = new CSharpCodeProvider())
            {
                var fileNames = buildFiles.AllClasses.Select(c => c.FullPath).ToArray();
                var references = buildFiles.References
                    .Select(reference => reference.FullPath)
                    .ToArray();

                var options = new CompilerParameters();
                options.ReferencedAssemblies.AddRange(references);
                var result = codeDomProvider.CompileAssemblyFromFile(options, fileNames);
                
                var errors = result.Errors.Cast<CompilerError>().Select(error => error.ErrorText);
                return new CompilationResult(errors, result.PathToAssembly);
            }

        }
    }
}