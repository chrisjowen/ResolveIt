using System.Linq;
using NUnit.Framework;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;
using ResolveIt.Tests.TestSyntaxHelpers;
using Rhino.Mocks;

namespace ResolveIt.Tests.Investigation
{
    [TestFixture]
    public class NRefactoryClassExaminerTests
    {
        private readonly IExamineSourceCodeForDependencies sourceExaminer = new NRefactorySourceCodeExaminer();
        private ICodeFileInfo codeFile;

        [SetUp]
        public void BeforeEachTest()
        {
            codeFile = MockRepository.GenerateStub<ICodeFileInfo>();
        }

        private void SetupCodeFile(string code)
        {
            codeFile.Stub(cf => cf.GetContent()).Return(code);
        }

        [Test]
        public void ShouldTellMeTheInfoAboutASuperClassOfAGivenClass()
        {
            SetupCodeFile(@"public class Foo : Bar {  }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.Count(), Is.EqualTo(2));
            Assert.That(types.HasADependencyCalled("Foo"));
            Assert.That(types.HasADependencyCalled("Bar"));
        }


        [Test]
        public void ShouldTellMeTheNameOfAnyInterfacesFoundOnAGivenClass()
        {
            SetupCodeFile(@"public class Foo : IBar {  }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.HasADependencyCalled("Foo"));
            Assert.That(types.HasADependencyCalled("IBar"));
        }

        [Test]
        public void ShouldTellMeAboutTheDependenciesAtFieldLevel()
        {
            SetupCodeFile(@"public class Foo { Bar b = new Bar(); }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.Count(), Is.EqualTo(2));
            Assert.That(types.HasADependencyCalled("Foo"));
            Assert.That(types.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeAboutTheDependenciesAtLocalVarLevel()
        {
            SetupCodeFile(@"public class Foo { public void FooMethod() { Bar b = new Bar(); } }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.Count(), Is.EqualTo(3));
            Assert.That(types.HasADependencyCalled("Foo"));
            Assert.That(types.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeTheNameOfAnyDepndenciesWhenCasting()
        {
            SetupCodeFile(@"public class Foo { public void FooMethod() { return (Bar)""abc""; } }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeTheNameOfAnyDepndenciesUsedInGenericObjectConstruction()
        {
            SetupCodeFile(@"public class Foo { public void FooMethod() { return new List<Bar>(); } }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.HasADependencyCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeTheNameOfAnyDepndenciesUsedInGenericConstructor()
        {
            SetupCodeFile(@"public class Foo<Bar> {  } }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.HasADependencyCalled("Bar"));
        }
    }


}