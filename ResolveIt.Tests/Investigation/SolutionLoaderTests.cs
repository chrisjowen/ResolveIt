using System.IO;
using System.Linq;
using NUnit.Framework;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;
using Rhino.Mocks;

namespace ResolveIt.Tests.Investigation
{
    [TestFixture]
    public class SolutionLoaderTests
    {
        private ILoadSolutionInfo solutionLoader;
        private ILoadProjectInfo projectLoader;
        const string PathToSolution = @"TestData\WellItCouldWork.sln";

        [SetUp]
        public void BeforeEachTest()
        {
            projectLoader = MockRepository.GenerateStub<ILoadProjectInfo>();
            solutionLoader = new SolutionLoader(projectLoader);
        }

        [Test]
        public void ShouldLoadSolutioProjectAndCodeFileInfoFromFile()
        {
            var project = new ProjectInfo("ProjectName", "Project", null);
            projectLoader
                .Stub(loader => loader.FromFile(Arg<FileInfo>.Matches(file => file.Name == "WellItCouldWork.csproj")))
                .Return(project);
            
            var solutionInfo = solutionLoader.FromFile(PathToSolution);
            Assert.That(solutionInfo.Name, Is.EqualTo("WellItCouldWork.sln"));
            Assert.That(solutionInfo.Path, Is.EqualTo(@"c:\projects\ResolveIt\ResolveIt.Tests\bin\Debug\TestData"));
            Assert.That(solutionInfo.Projects.Count, Is.EqualTo(1));

            var projectInfo = solutionInfo.Projects.First();
            Assert.That(projectInfo.Name, Is.EqualTo(project.Name));
            Assert.That(projectInfo.Path, Is.EqualTo(project.Path));
            Assert.That(projectInfo.Solution, Is.EqualTo(solutionInfo));

        }
    }
}
