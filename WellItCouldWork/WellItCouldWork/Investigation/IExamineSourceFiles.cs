using System.Collections.Generic;

namespace WellItCouldWork.Investigation
{
    public interface IExamineSourceFiles
    {
        IList<TypeInfo> ExamineSource(string code);
    }
}