using System.Collections.Generic;
using ResolveIt.Core.Investigation;

namespace ResolveIt.Core.Model
{
    public interface ISolutionInfo
    {
        string Name { get;  }
        string Path { get;  }
        IList<IProjectInfo> Projects { get;  }
        ICodeFileInfo FindCodeFileFor(IDependencyInfo dependencyInfo);
    }
}