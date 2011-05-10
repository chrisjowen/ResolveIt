using System.Collections.Generic;
using System.Linq;
using WellItCouldWork.Investigation;

namespace WellItCouldWork.BuildCreation
{
    public class BuildMonkey 
    {
        private readonly IExamineSourceFiles sourceExaminer;
        private readonly ISolutionFile solutionFile;
        private readonly IGetSourceCode sourceCodeRepo;
        private BuildFiles buildFiles;

        public static BuildMonkey Using(IExamineSourceFiles examiner, ISolutionFile solutionFile, IGetSourceCode getSourceCode)
        {
            return new BuildMonkey(examiner, solutionFile, getSourceCode);
        }

        private BuildMonkey(IExamineSourceFiles sourceExaminer, ISolutionFile solutionFile, IGetSourceCode sourceCodeRepo)
        {
            this.sourceExaminer = sourceExaminer;
            this.solutionFile = solutionFile;
            this.sourceCodeRepo = sourceCodeRepo;
        }

        public BuildFiles WhatFilesAreRequiredToBuild(TypeInfo type)
        {
            return WhatFilesAreRequiredToBuild(solutionFile.FindClassByType(type));
        }

        public BuildFiles WhatFilesAreRequiredToBuild(Class @class)
        {
            var solutionsReferences = solutionFile.AllReferences ?? new List<Reference>();
            buildFiles =  new BuildFiles(@class, ResolveClassesRequiredFor(@class), solutionsReferences);
            return buildFiles;
        }

        private IList<Class> ResolveClassesRequiredFor(Class @class)
        {
            IList<Class> classDependencies = new List<Class>();
            var source = sourceCodeRepo.SourceFor(@class);
            var typesFoundInFile = sourceExaminer.ExamineSource(source);
            if (typesFoundInFile!=null)
                classDependencies = MatchSolutionClasses(typesFoundInFile);

            return classDependencies;
        }

        private IList<Class> MatchSolutionClasses(IEnumerable<TypeInfo> types)
        {
            return types.Select(t => solutionFile.FindClassByType(t)).Where(c => c!=null).ToList();
        }
    }
}
