using System.IO;

namespace WellItCouldWork
{
    public class Class
    {
        public string ClassName { get; private set; }
        public string Path { get; private set; }

        private Class(string className, string path)
        {
            ClassName = className;
            Path = path;
        }

        public Class(string className) 
            : this(className, string.Empty)
        {
        }

        public static Class FromPath(string path)
        {
            var fileInfo = new FileInfo(path);
            return new Class(fileInfo.Name, fileInfo.DirectoryName);
        }

        public string FullPath
        {
            get { return string.Format("{0}\\{1}", Path, ClassName); }
        }
    }
}