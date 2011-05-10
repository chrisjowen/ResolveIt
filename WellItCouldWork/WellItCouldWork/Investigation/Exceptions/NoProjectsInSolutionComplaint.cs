using System;

namespace WellItCouldWork.Investigation.Exceptions
{
    public class NoProjectsInSolutionComplaint : ApplicationException
    {
        public NoProjectsInSolutionComplaint(ISolutionFile solution)
            : base(string.Format("No projects found in the given solution: {0}", solution.Name))
        {

        }
    }
}