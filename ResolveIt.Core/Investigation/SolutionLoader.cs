using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ResolveIt.Core.Model;

namespace ResolveIt.Core.Investigation
{
    public class SolutionLoader : ILoadSolutionInfo
    {
        private readonly ILoadProjectInfo projectLoader;

        public SolutionLoader(ILoadProjectInfo projectLoader)
        {
            this.projectLoader = projectLoader;
        }

        public ISolutionInfo FromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Unable to find file", filePath);

            var file = new FileInfo(filePath);
            var solution = new SolutionInfo(file.Name, file.DirectoryName);

            foreach (var project in ResolveProjectFilesFrom(file))
                solution.AddProject(new ProjectInfo(project.Name, project.Path, solution));

            return solution;
        }

        private IEnumerable<IProjectInfo> ResolveProjectFilesFrom(FileInfo file)
        {
            IEnumerable<IProjectInfo> projects;
            using (var reader = new StreamReader(file.OpenRead()))
            {
                var content = reader.ReadToEnd();
                var matches = new Regex("[.A-Za-z0-9\\\\]*.csproj").Matches(content);

                projects = matches.Cast<Match>()
                    .Select(match => projectLoader.FromFile(new FileInfo(string.Format("{0}\\{1}", file.DirectoryName, match.Value))));
            }
            return projects;
        }
    }
}
