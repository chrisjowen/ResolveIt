using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class NRefactoryDeclerationExaminer : IExamineSourceCodeForDeclerations
    {
        public IEnumerable<IDeclerationInfo> ExamineSource(ICodeFileInfo codeFile)
        {
            var result = GetCompilationResult(codeFile.GetContent());
            var nodes = result.Flatten();

            return nodes.Select(node => new DependencyTypeFactory().DeclerationFor(node, codeFile))
                .Where(dependency => dependency != null)
                .Distinct(new DeclerationComparer()).ToList();
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
}