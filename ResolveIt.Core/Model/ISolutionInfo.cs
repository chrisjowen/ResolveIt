using System.Collections.Generic;

namespace ResolveIt.Core.Model
{
    public interface ISolutionInfo
    {
        string Name { get;  }
        string Path { get;  }
        IList<IProjectInfo> Projects { get;  }
    }
}