using System.IO;
using System.Linq;
using NUnit.Framework;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;
using Rhino.Mocks;

namespace ResolveIt.Tests.Investigation
{
    [TestFixture]
    public class ProjectLoaderTests
    {
        private ILoadProjectInfo projectLoader;
        private ILoadCodeFileInfo codeFileLoader;
        const string PathToProject = @"TestData\WellItCouldWork\WellItCouldWork.csproj";

        [SetUp]
        public void BeforeEachTest()
        {
            codeFileLoader = MockRepository.GenerateStub<ILoadCodeFileInfo>();
            projectLoader = new ProjectLoader(codeFileLoader);
        }

        [Test]
        public void ShouldLoadProjectInfoFromFile()
        {
            codeFileLoader.Stub(loader => loader.FromFile(Arg<FileInfo>.Is.Anything)).Do(new CodeFileLoaderDelegate(Get));

            var file = new FileInfo(PathToProject);
            var projectInfo = projectLoader.FromFile(file);
            Assert.That(projectInfo.Name, Is.EqualTo("WellItCouldWork.csproj"));
            Assert.That(projectInfo.Path, Is.EqualTo(@"c:\projects\ResolveIt\ResolveIt.Tests\bin\Debug\TestData\WellItCouldWork"));
            
            Assert.That(projectInfo.CodeFiles.Count, Is.EqualTo(2));
            Assert.That(projectInfo.CodeFiles.First().Name, Is.EqualTo("NonRootPathClass.cs"));
            Assert.That(projectInfo.CodeFiles.First().Path, Is.EqualTo(@"c:\projects\ResolveIt\ResolveIt.Tests\bin\Debug\TestData\WellItCouldWork\FolderPath"));
            Assert.That(projectInfo.CodeFiles.First().Project, Is.EqualTo(projectInfo));

            Assert.That(projectInfo.CodeFiles.Last().Name, Is.EqualTo("RootPathClass.cs"));
            Assert.That(projectInfo.CodeFiles.Last().Project, Is.EqualTo(projectInfo));
            Assert.That(projectInfo.CodeFiles.Last().Path, Is.EqualTo(@"c:\projects\ResolveIt\ResolveIt.Tests\bin\Debug\TestData\WellItCouldWork"));

            Assert.That(projectInfo.References.Count(), Is.EqualTo(2));
            Assert.That(projectInfo.References.First().Name, Is.EqualTo("ExternalAssembly.dll"));
            Assert.That(projectInfo.References.Last().Name, Is.EqualTo("GacAssembly"));
                        
        }

        public ICodeFileInfo Get(FileInfo fileInfo)
        {
            return new CodeFileInfo(fileInfo.Name, fileInfo.DirectoryName);
        }

        private delegate ICodeFileInfo CodeFileLoaderDelegate(FileInfo file);
    }
}