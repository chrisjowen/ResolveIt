using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;

namespace WellItCouldWork.Investigation
{
    public class NRefactorySourceExaminer : IExamineSourceFiles
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

        public IList<TypeInfo> ExamineSource(string code)
        {
            var result = GetCompilationResult(code);
            var nodes = result.Flatten();

            return (from node in nodes 
                    let type = new TypeFactory().For(node)
                    where type != null 
                    select type).Distinct(new TypeNameComparer()).ToList();
        }
    }
}