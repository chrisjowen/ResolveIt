namespace ResolveIt.Core.Model
{
    public class DeclerationInfo : IDeclerationInfo
    {
        public string Name { get; private set; }
        public ICodeFileInfo CodeFileInfo { get; private set; }

        public DeclerationInfo(string name, ICodeFileInfo codeFileInfo)
        {
            Name = name;
            CodeFileInfo = codeFileInfo;
        }
    }
}
