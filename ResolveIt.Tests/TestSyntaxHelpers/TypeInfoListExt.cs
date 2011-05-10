using System.Collections.Generic;
using System.Linq;
using ResolveIt.Core.Model;

namespace ResolveIt.Tests.TestSyntaxHelpers
{
    public static class TypeInfoListExt
    {
        public static bool HasADependencyCalled(this IEnumerable<IDependencyInfo> classes, string name)
        {
            return classes.Any(c => c.Name == name);
        }
    }
}