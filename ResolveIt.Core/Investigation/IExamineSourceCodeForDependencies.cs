using System.Collections.Generic;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public interface IExamineSourceCodeForDependencies
    {
        IEnumerable<IDependencyInfo> ExamineSource(ICodeFileInfo codeFile);
    }
}