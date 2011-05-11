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
            var dependencyInfos = ExamineSource(codeFile, solution, new List<ICodeFileInfo>()).Distinct(new DependencyComparer());
            return dependencyInfos;
        }

        private IEnumerable<IDependencyInfo> ExamineSource(ICodeFileInfo codeFile, ISolutionInfo solution, IList<ICodeFileInfo> parsedCodeFiles)
        {
            var result = GetCompilationResult(codeFile.GetContent());
            var nodes = result.Flatten();


            var dependencies = nodes.Select(node => new DependencyTypeFactory().For(node))
                .Where(dependency => dependency != null)
                .Distinct(new DependencyComparer()).ToList();

            parsedCodeFiles.Add(codeFile);

            foreach (var dependencyInfo in dependencies)
            {
                dependencyInfo.CodeFileInfo = solution.FindCodeFileFor(dependencyInfo);
            }

            var codeFileInfos = dependencies
                .Select(dependencyInfo => dependencyInfo.CodeFileInfo)
                .Where(codeFileInfo => codeFileInfo != null && !parsedCodeFiles.Contains(codeFileInfo))
                .Distinct().ToList();

            foreach (var code in codeFileInfos)
            {
                dependencies.AddRange(ExamineSource(code, solution, parsedCodeFiles));
            }

            return dependencies; 
        }
    }
}