using System.CodeDom.Compiler;
using System.Linq;
using Microsoft.CSharp;

namespace ResolveIt.Core.BuildCreation
{
    public class BuildFilesCompiler : ICompileBuildFiles
    {
        public CompilationResult Compile(BuildCodeFiles buildFiles)
        {
            using(var codeDomProvider = new CSharpCodeProvider())
            {
                var fileNames = buildFiles.AllFiles.Select(c => c.FullPath).Distinct().ToList();

                var references = buildFiles.References
                    .Select(reference => reference.FullPath)
                    .ToArray();

                var options = new CompilerParameters();
                options.ReferencedAssemblies.AddRange(references);
                var result = codeDomProvider.CompileAssemblyFromFile(options, fileNames.ToArray());
                
                var errors = result.Errors.Cast<CompilerError>().Select(error => error.ErrorText);
                return new CompilationResult(errors, result.PathToAssembly);
            }

        }
    }
}