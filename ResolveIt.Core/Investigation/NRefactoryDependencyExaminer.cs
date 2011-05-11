using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class NRefactoryDependencyExaminer : IExamineSourceCodeForDependencies
    {
        private static CompilationUnit GetCompilationResult(string sourceCode)
        {
            using (var parser = ParserFactory.CreateParser(SupportedLanguage.CSharp, new StringReader(sourceCode)))
            {
                parser.Parse();
                var result = parser.CompilationUnit;
                return result;
            }
        }

        public IEnumerable<IDependencyInfo> ExamineSource(ICodeFileInfo codeFile, ISolutionInfo solution)
        {
            var result = GetCompilationResult(codeFile.GetContent());
            var nodes = result.Flatten();
            var resolvedDependencies = new List<IDependencyInfo>();


            var dependencies =  nodes.Select(node => new DependencyTypeFactory().For(node))
                .Where(dependency => dependency != null)
                .Distinct(new DependencyComparer()).ToList();

            foreach (var dependencyInfo in dependencies)
            {
                dependencyInfo.CodeFileInfo = solution.FindCodeFileFor(dependencyInfo);
                resolvedDependencies.Add(dependencyInfo);
            }

            var codeFileInfos = dependencies
                .Where(d => !resolvedDependencies.Contains(d))
                .Select(d=> d.CodeFileInfo)
                .Where(d => d!=null)
                .Distinct();

            foreach (var code in codeFileInfos)
            {
                dependencies.AddRange(ExamineSource(code, solution));
            }

            return dependencies;
        }
    }
}