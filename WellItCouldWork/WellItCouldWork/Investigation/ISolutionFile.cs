using System.Collections.Generic;

namespace WellItCouldWork.Investigation
{
    public interface ISolutionFile
    {
        string Name { get; }
        string Path { get; }
        IList<IProjectFile> Projects { get; }
        IList<Reference> AllReferences { get; }
        Class FindClassByType(TypeInfo type);
    }
}