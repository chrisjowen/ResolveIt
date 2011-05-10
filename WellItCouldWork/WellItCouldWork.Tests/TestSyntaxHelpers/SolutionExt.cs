using System.Linq;
using WellItCouldWork.Investigation;

namespace WellItCouldWork.Tests.TestSyntaxHelpers
{
    public static class SolutionExt
    {
        public static bool ContainsAProjectCalled(this SolutionFile solutionFile, string projectName)
        {
            return solutionFile.Projects.Any(p => p.ProjectName == projectName);
        }
    }
}