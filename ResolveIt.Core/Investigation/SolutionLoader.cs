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
        private SolutionInfo solution;

        public SolutionLoader(ILoadProjectInfo projectLoader)
        {
            this.projectLoader = projectLoader;
        }

        public ISolutionInfo FromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Unable to find file", filePath);

            var file = new FileInfo(filePath);
            solution = new SolutionInfo(file.Name, file.DirectoryName);

            foreach (var project in ResolveProjectFilesFrom(file))
                solution.AddProject(project);

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
                    .Where(match => File.Exists(string.Format("{0}\\{1}", file.DirectoryName, match.Value)))
                    .Select(match => ProjectInfoFrom(file, match));
            }
            return projects;
        }

        private IProjectInfo ProjectInfoFrom(FileInfo file, Match match)
        {
            var projectInfo = projectLoader.FromFile(new FileInfo(string.Format("{0}\\{1}", file.DirectoryName, match.Value)));
            projectInfo.Solution = solution;
            return projectInfo;
        }
    }
}
