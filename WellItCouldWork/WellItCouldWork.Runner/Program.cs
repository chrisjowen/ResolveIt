using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WellItCouldWork.BuildCreation;
using WellItCouldWork.Investigation;
using WellItCouldWork.TestRunner;

namespace WellItCouldWork.Runner
{
    class Program
    {
        private static string tempDllName;
        private static string solutionFilePath;
        private static string testFilePath;
        private static readonly string TempPath = string.Format("{0}//WellItCouldWork//", Path.GetTempPath());

        static void Main(string[] args)
        {
            if(args.Count() != 2)
            {
                Console.WriteLine("Solution and Test file required");
                return;
            }

            solutionFilePath = args[0];
            testFilePath = args[1];

            BeginTestExecution(solutionFilePath, testFilePath);

            new Watcher(testFilePath, () => BeginTestExecution(solutionFilePath, testFilePath));

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Started watching file.");
            Console.WriteLine("-------------------------------\n");
            Console.WriteLine("Press \'q\' to quit.");

            while (Console.Read() != 'q') {}
        }

        private static void BeginTestExecution(string solutionDir, string testFileDir)
        {
            ClearTempPath();
  
            var type = GetLookupType(testFileDir);
            var solution = SolutionFile.Load(solutionDir);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDetermening files To compile");
            Console.WriteLine("-------------------------------");

            var buildFiles = BuildMonkey.Using(new NRefactorySourceExaminer(), solution, new SourceFromFileRepository())
                .WhatFilesAreRequiredToBuild(type);

            CopyBuildFilesToTempDir(buildFiles);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nCompiling files:");
            Console.WriteLine("-------------------------------");

            var buildFilesCompiler = new BuildFilesCompiler();
            var result = buildFilesCompiler.Compile(buildFiles);
            ProcessResult(result);
        }

        private static void CopyBuildFilesToTempDir(BuildFiles buildFiles)
        {
            Console.WriteLine("\nFound classes:");
            Console.WriteLine("-------------------------------");

            foreach(var dependentClass in buildFiles.DependentClasses) 
                Console.WriteLine(" - " + dependentClass.ClassName);

            Console.WriteLine("\nFound references:");
            Console.WriteLine("-------------------------------");

            CopyReferencesToTempDir(buildFiles.References.Where(r => r.IsExternalAssembily));
        }

        private static void ClearTempPath()
        {
            if(Directory.Exists(TempPath))
                Directory.Delete(TempPath, true);
            Directory.CreateDirectory(TempPath);
        }

        private static void CopyReferencesToTempDir(IEnumerable<Reference> references)
        {
            foreach (var reference in references)
            {
                var copyToPath = string.Format("{0}{1}", TempPath, reference.Filename);
                Console.WriteLine(string.Format(" - Copying '{0}' to '{1}'", reference.FullPath, copyToPath));
                File.Copy(reference.FullPath, copyToPath, true);
            }
        }

        private static void ProcessResult(CompilationResult result)
        {
            if (result.HasErrors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                ProcesErrors(result);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                ProcessSucessfullBuild(result);
            }
        }

        private static void ProcessSucessfullBuild(CompilationResult result)
        {
            Console.WriteLine("SUCCESS");

            tempDllName = string.Format("{0}{1}", TempPath, "tmp.dll");
            File.Copy(result.AssemblyLocation, string.Format(tempDllName, TempPath));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nRunning Tests");
            Console.WriteLine("-------------------------------");

            IRunUnitTests testRunner = new NUnitConsoleRunner(tempDllName);
            Console.WriteLine(testRunner.RunAllTests());
        }

        private static void ProcesErrors(CompilationResult result)
        {
            Console.WriteLine("ERROR: compilation failed");
            foreach (var error in result.Errors)
                Console.WriteLine("\t" + error);
        }

        private static TypeInfo GetLookupType(string testFileDir)
        {
            var fileName = new FileInfo(testFileDir).Name;
            fileName = fileName.Replace(".cs", "");
            return fileName;
        }
    }
}
