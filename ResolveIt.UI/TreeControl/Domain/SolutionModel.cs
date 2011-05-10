using System.Collections.Generic;

namespace ResolveIt.UI.TreeControl.Domain
{
    public class SolutionModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IList<ProjectModel> Projects { get; set; }

        public SolutionModel(string name, string path)
        {
            Name = name;
            Path = path;
            Projects = new List<ProjectModel>();
        }
    }

    public class ProjectModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public SolutionModel Solution { get; set; }
        public IList<FileModel> Files { get; set; }

        public ProjectModel(string name, string path, SolutionModel solution)
        {
            Name = name;
            Path = path;
            Solution = solution;
            Files = new List<FileModel>();
        }

    }

    public class FileModel
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public ProjectModel Project { get; set; }

        public FileModel(string name, string path, ProjectModel project)
        {
            Name = name;
            Path = path;
            Project = project;
        }
    }
}
