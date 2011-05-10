using System;
using System.IO;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public interface ILoadProjectInfo
    {
        IProjectInfo FromFile(FileInfo file);
    }

    public interface ILoadCodeFileInfo
    {
        ICodeFileInfo FromFile(FileInfo file);
    }

    class LoadCodeFileInfo : ILoadCodeFileInfo
    {
        public ICodeFileInfo FromFile(FileInfo file)
        {
            throw new NotImplementedException();
        }
    }
}