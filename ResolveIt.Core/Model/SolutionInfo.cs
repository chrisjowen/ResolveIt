using System;
using System.Collections.Generic;
using System.Linq;
using ResolveIt.Core.Investigation;

namespace ResolveIt.Core.Model
{
    public class SolutionInfo : ISolutionInfo
    {
        private readonly IList<IProjectInfo> projects = new List<IProjectInfo>();

        public string Name { get; private set; }
        public string Path { get; private set; }

        public SolutionInfo(string name, string path)
        {
            Path = path;
            Name = name;
        }

        public void AddProject(IProjectInfo project)
        {
            projects.Add(project);
        }

        public IList<IProjectInfo> Projects
        {
            get { return new List<IProjectInfo>(projects); }
        }

        public ICodeFileInfo FindCodeFileFor(IDependencyInfo dependencyInfo)
        {
            return projects.SelectMany(p => p.CodeFiles)
                .FirstOrDefault(c => c.Declerations.Any(d => d.Name == dependencyInfo.Name));
        }
    }
}