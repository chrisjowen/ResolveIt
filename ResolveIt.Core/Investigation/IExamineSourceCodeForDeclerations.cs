using System.Collections.Generic;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public interface IExamineSourceCodeForDeclerations
    {
        IEnumerable<IDeclerationInfo> ExamineSource(ICodeFileInfo codeFile);
    }

    public interface IExamineSourceCodeForDependencies
    {
        IEnumerable<IDependencyInfo> ExamineSource(ICodeFileInfo codeFile, ISolutionInfo solution);
    }
}