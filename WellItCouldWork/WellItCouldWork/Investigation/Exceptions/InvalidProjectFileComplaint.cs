using System;

namespace WellItCouldWork.Investigation.Exceptions
{
    public class InvalidProjectFileComplaint : ApplicationException
    {
        public InvalidProjectFileComplaint(IProjectFile projectFile)
            : base(string.Format("Invalid project file found: {0}", projectFile.ProjectName))
        {
            
        }
    }
}