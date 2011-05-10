using System.Linq;
using WellItCouldWork.Investigation;

namespace WellItCouldWork.Tests.TestSyntaxHelpers
{
    public static class ProjectExt
    {
        public static bool HasAReferenceCalled(this ProjectFile projectFile, string referenceAssemblyName)
        {
            return projectFile.References.Any(reference => reference.FullPath == referenceAssemblyName);
        }

        public static bool HasAClassCalled(this ProjectFile projectFile, string className)
        {
            return projectFile.Classes.Any(reference => reference.ClassName == className);
        }
    }
}
