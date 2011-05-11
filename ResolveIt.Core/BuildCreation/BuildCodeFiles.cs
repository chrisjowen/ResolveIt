using System.Collections.Generic;
using System.Linq;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.BuildCreation
{
    public class BuildCodeFiles
    {
        public ICodeFileInfo Target { get; private set; }
        public IEnumerable<IDependencyInfo> Dependencies { get; private set; }
        public IEnumerable<IReferenceInfo> References { get; private set; }

        public BuildCodeFiles(ICodeFileInfo target, IEnumerable<IDependencyInfo> dependencies, IEnumerable<IReferenceInfo> references)
        {
            Target = target;
            Dependencies = dependencies;
            References = references;
        }

        public IEnumerable<ICodeFileInfo> AllFiles
        {
            get
            {
                var codeFiles = new List<ICodeFileInfo> {Target};
                if (Dependencies!=null)
                    codeFiles.AddRange(Dependencies.Where(c => c != null).Select(d => d.CodeFileInfo));
                return codeFiles.Where(c => c!=null).Distinct();
            }
        }
    }
}
