using System.Collections.Generic;
using System.Linq;
using WellItCouldWork.BuildCreation;

namespace WellItCouldWork.Tests.TestSyntaxHelpers
{
    public static class TypeInfoListExt
    {
        public static bool HasATypeCalled(this IList<TypeInfo> classes, string name)
        {
            return classes.Any(c => c.Name == name);
        }
    }

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