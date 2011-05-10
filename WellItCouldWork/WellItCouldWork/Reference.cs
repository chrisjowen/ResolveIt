using System;

namespace WellItCouldWork
{
    public class Reference
    {
        private readonly string path;
        private readonly string fileName;

        public Reference(string fileName)
            : this(string.Empty, fileName)
        {
        }

        public Reference(string path, string fileName)
        {
            this.path = path;
            this.fileName = fileName;
        }

        public string FullPath
        {
            get
            {
                return !string.IsNullOrEmpty(path) ? path + "\\" + Filename : Filename;
            }
        }

        public bool IsExternalAssembily
        {
            get { return !string.IsNullOrEmpty(path); } 
        }

        public string Filename
        {
            get { return fileName.Contains(".dll") ? fileName : fileName + ".dll"; }
        }
    }
}