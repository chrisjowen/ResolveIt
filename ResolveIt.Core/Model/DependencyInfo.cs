namespace ResolveIt.Core.Model
{
    public class DependencyInfo : IDependencyInfo
    {
        public string Name { get; private set; }
        public ICodeFileInfo CodeFileInfo { get; private set; }

        public DependencyInfo(string name, ICodeFileInfo codeFileInfo)
        {
            Name = name;
            CodeFileInfo = codeFileInfo;
        }
    }
}
