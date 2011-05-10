using System;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Exceptions
{
    public class InvalidProjectFileComplaint : ApplicationException
    {
        public InvalidProjectFileComplaint(ProjectInfo projectFile)
            : base(string.Format("Invalid project file found: {0}", projectFile.Name))
        {

        }
    }
}