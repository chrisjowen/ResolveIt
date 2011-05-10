using System.Collections.Generic;
using System.Linq;

namespace WellItCouldWork.BuildCreation
{
    public class BuildFiles
    {
        public BuildFiles(Class target, IList<Class> dependentClasses, IList<Reference> references)
        {
            Target = target;
            DependentClasses = dependentClasses;
            References = references;
        }

        public Class Target { get; private set; }
        public IList<Class> DependentClasses { get; private set; }
        public IList<Reference> References { get; private set; }

        public IList<Class> AllClasses
        {
            get { return new List<Class> { Target }.Concat(DependentClasses).ToList().AsReadOnly(); }
        }
    }
}

