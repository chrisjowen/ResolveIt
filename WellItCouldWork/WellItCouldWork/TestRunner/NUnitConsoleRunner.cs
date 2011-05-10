using System;
using System.Diagnostics;

namespace WellItCouldWork.TestRunner
{
    public class NUnitConsoleRunner : IRunUnitTests
    {
        private readonly string assembilyPath;

        public NUnitConsoleRunner(string assembilyPath)
        {
            this.assembilyPath = assembilyPath;
        }

        public string RunAllTests()
        {
            var nunitExe = Environment.CurrentDirectory + "\\..\\..\\..\\Tools\\Nunit\\Nunit-console.exe";
            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = nunitExe,
                Arguments = assembilyPath,
                CreateNoWindow = true
            };
            var output = string.Empty;

            using (var p = new Process())
            {
                p.StartInfo = processStartInfo;
                p.Start();
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }

            return output;
        }
    }
}