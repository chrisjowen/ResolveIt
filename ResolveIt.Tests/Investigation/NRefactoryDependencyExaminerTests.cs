using System.Linq;
using NUnit.Framework;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;
using ResolveIt.Tests.TestSyntaxHelpers;
using Rhino.Mocks;

namespace ResolveIt.Tests.Investigation
{
    [TestFixture]
    public class NRefactoryDependencyExaminerTests
    {
        private readonly IExamineSourceCodeForDependencies sourceExaminer = new NRefactoryDependencyExaminer();
        private ICodeFileInfo codeFile;
        private SolutionInfo solution;

        [SetUp]
        public void BeforeEachTest()
        {
            codeFile = MockRepository.GenerateStub<ICodeFileInfo>();
            solution = new SolutionInfo(null, null);
        }

        private void SetupCodeFile(string code)
        {
            codeFile.Stub(cf => cf.GetContent()).Return(code);
        }

        [Test]
        public void ShouldTellMeTheInfoAboutASuperClassOfAGivenClass()
        {
            SetupCodeFile(@"public class Foo : Bar {  }");
            var dependencies = sourceExaminer.ExamineSource(codeFile, solution);
            Assert.That(dependencies.Count(), Is.EqualTo(1));
            Assert.That(dependencies.HasADependencyCalled("Bar"));
        }


        [Test]
        public void ShouldTellMeTheNameOfAnyInterfacesFoundOnAGivenClass()
        {
            SetupCodeFile(@"public class Foo : IBar {  }");
            var dependencies = sourceExaminer.ExamineSource(codeFile, solution);
            Assert.That(dependencies.HasADependencyCalled("IBar"));
        }

        [Test]
        public void ShouldTellMeAboutTheDependenciesAtFieldLevel()
        {
            SetupCodeFile(@"public class Foo { Bar b = new Bar(); }");
            var dependencies = sourceExaminer.ExamineSource(codeFile, solution);
            Assert.That(dependencies.Count(), Is.EqualTo(1));
            Assert.That(dependencies.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeAboutTheDependenciesAtLocalVarLevel()
        {
            SetupCodeFile(@"public class Foo { public void FooMethod() { Bar b = new Bar(); } }");
            var dependencies = sourceExaminer.ExamineSource(codeFile, solution);
            Assert.That(dependencies.Count(), Is.EqualTo(2));
            Assert.That(dependencies.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeTheNameOfAnyDepndenciesWhenCasting()
        {
            SetupCodeFile(@"public class Foo { public void FooMethod() { return (Bar)""abc""; } }");
            var dependencies = sourceExaminer.ExamineSource(codeFile, solution);
            Assert.That(dependencies.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeTheNameOfAnyDepndenciesUsedInGenericObjectConstruction()
        {
            SetupCodeFile(@"public class Foo { public void FooMethod() { return new List<Bar>(); } }");
            var dependencies = sourceExaminer.ExamineSource(codeFile, solution);
            Assert.That(dependencies.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeTheNameOfAnyDepndenciesUsedInGenericConstructor()
        {
            SetupCodeFile(@"public class Foo<Bar> {  } }");
            var dependencies = sourceExaminer.ExamineSource(codeFile, solution);
            Assert.That(dependencies.HasADependencyCalled("Bar"));
        }
    }
}