using System.Collections.Generic;
using System.Linq;

namespace WellItCouldWork.SyntaxHelpers
{
    public static class GenericListExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return list.Count() == 0;
        }        
        
        public static bool IsNotEmpty<T>(this IEnumerable<T> list)
        {
            return !list.IsEmpty();
        }
    }
}