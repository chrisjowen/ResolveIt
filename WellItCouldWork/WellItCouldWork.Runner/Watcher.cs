using System;
using System.IO;

namespace WellItCouldWork.Runner
{
    public class Watcher
    {
        private readonly Action onChangedAction;
        private readonly string fileName;

        public Watcher(string fileName, Action onChangedAction)
        {
            this.fileName = fileName;
            this.onChangedAction = onChangedAction;
            SetupWatcher();
        }

        private void SetupWatcher()
        {
            var fileInfo = new FileInfo(fileName);
            if (fileInfo.Directory == null)
                throw new IOException("File Path has no directory to watch");

            var incoming = new FileSystemWatcher
                               {
                                   NotifyFilter = NotifyFilters.Size,
                                   Path = fileInfo.Directory.FullName,
                                   Filter = fileInfo.Name
                               };
            incoming.Changed += IncomingChanged;
            incoming.EnableRaisingEvents = true;
        }

        private void IncomingChanged(object sender, FileSystemEventArgs e)
        {
            onChangedAction.DynamicInvoke();
        }



    }
}