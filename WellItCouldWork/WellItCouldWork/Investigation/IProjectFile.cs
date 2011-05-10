using System.Collections.Generic;

namespace WellItCouldWork.Investigation
{
    public interface IProjectFile
    {
        string ProjectLocation { get; }
        string ProjectName { get; }
        IList<Reference> References { get; }
        IList<Class> Classes { get; }
    }
}