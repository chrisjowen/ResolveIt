using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class NRefactorySourceCodeExaminer : IExamineSourceCodeForDependencies
    {
        public IEnumerable<IDependencyInfo> ExamineSource(ICodeFileInfo codeFile)
        {
            var result = GetCompilationResult(codeFile.GetContent());
            var nodes = result.Flatten();

            return (from node in nodes
                    let type = new DependencyTypeFactory().For(node, codeFile)
                    where type != null
                    select type).Distinct(new DependencyComparer()).ToList();
        }

        private static CompilationUnit GetCompilationResult(string sourceCode)
        {
            using (var parser = ParserFactory.CreateParser(SupportedLanguage.CSharp, new StringReader(sourceCode)))
            {
                parser.Parse();
                var result = parser.CompilationUnit;
                return result;
            }
        }
    }

    public class DependencyComparer : IEqualityComparer<IDependencyInfo>
    {
        public bool Equals(IDependencyInfo x, IDependencyInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(IDependencyInfo obj)
        {
            return 1;
        }
    }
}