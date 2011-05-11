using System.Collections.Generic;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class DeclerationComparer : IEqualityComparer<IDeclerationInfo>
    {
        public bool Equals(IDeclerationInfo x, IDeclerationInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(IDeclerationInfo obj)
        {
            return 1;
        }
    }

    public class DependencyComparer : IEqualityComparer<IDependencyInfo>
    {
        public bool Equals(IDependencyInfo x, IDependencyInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(IDependencyInfo obj)
        {
            return 1;
        }
    }
}