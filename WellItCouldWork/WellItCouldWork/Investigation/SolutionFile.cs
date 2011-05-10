using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WellItCouldWork.Investigation.Exceptions;
using WellItCouldWork.SyntaxHelpers;

namespace WellItCouldWork.Investigation
{
    public class SolutionFile : ISolutionFile
    {
        private readonly IList<IProjectFile> projects = new List<IProjectFile>();
        
        public string Name { get; private set; }
        public string Path { get; private set; }

        public static SolutionFile Load(string filePath)
        {
            return new SolutionFile(new FileInfo(filePath));
        }

        private SolutionFile(FileInfo file)
        {
            Path = file.DirectoryName;
            Name = file.Name;
            projects = ResolveProjectFilesFrom(file);
        }

        private IList<IProjectFile> ResolveProjectFilesFrom(FileInfo file)
        {
            using (var fileStream = file.OpenRead())
            {
                using(var reader = new StreamReader(fileStream))
                {
                    return ResolveProjectFilesFrom(reader.ReadToEnd());
                }
            }
        }

        private IList<IProjectFile> ResolveProjectFilesFrom(string content)
        {
            var matches = new Regex("[.A-Za-z0-9\\\\]*.csproj").Matches(content);
            
            var projectFiles =  matches.Cast<Match>()
                    .Select(match => ProjectFile.Load(string.Format("{0}\\{1}", Path, match.Value)))
                    .Cast<IProjectFile>().ToList();

            if (projectFiles.IsEmpty()) throw new NoProjectsInSolutionComplaint(this);
            return projectFiles;
        }

        public IList<IProjectFile> Projects
        {
            get { return new List<IProjectFile>(projects); }
        }

        public IList<Reference> AllReferences
        {
            get { return Projects.SelectMany(p => p.References).ToList(); }
        }

        public Class FindClassByType(TypeInfo type)
        {
            var allClasses = string.Join("'", AllClasses.Select(c => c.ClassName));
            return AllClasses.FirstOrDefault(c => c.ClassName == type.Name + ".cs");
        }
        
        private IEnumerable<Class> AllClasses
        {
            get { return Projects.SelectMany(p => p.Classes); }
        }
    }
}