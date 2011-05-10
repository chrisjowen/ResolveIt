using System.Linq;
using WellItCouldWork.BuildCreation;

namespace ResolveIt.Tests.TestSyntaxHelpers
{
    public static class BuildFilesExt
    {
        public static bool HasAClassCalled(this BuildFiles buildFiles, string name)
        {
            return buildFiles.DependentClasses.Any(c => c.ClassName == name);
        }

        public static bool HasAReferenceCalled(this BuildFiles buildFiles, string name)
        {
            return buildFiles.References.Any(c => c.FullPath == name);
        }
    }
}