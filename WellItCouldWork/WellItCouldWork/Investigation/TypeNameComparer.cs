using System.Collections.Generic;

namespace WellItCouldWork.Investigation
{
    public class TypeNameComparer : IEqualityComparer<TypeInfo>
    {
        public bool Equals(TypeInfo x, TypeInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(TypeInfo obj)
        {
            return 1;
        }
    }
}