using System.Collections.Generic;
using System.Linq;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;

namespace ResolveIt.Tests.TestSyntaxHelpers
{
    public static class DeclerationsExt
    {
        public static bool HasADeclerationCalled(this IEnumerable<IDeclerationInfo> declerations, string name)
        {
            return declerations.Any(c => c.Name == name);
        }
    }

    public static class DependenciesExt
    {
        public static bool HasADependencyCalled(this IEnumerable<IDependencyInfo> dependencies, string name)
        {
            return dependencies.Any(c => c.Name == name);
        }
    }
}