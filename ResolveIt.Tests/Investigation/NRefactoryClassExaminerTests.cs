using System.Linq;
using NUnit.Framework;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;
using ResolveIt.Tests.TestSyntaxHelpers;
using Rhino.Mocks;

namespace ResolveIt.Tests.Investigation
{
    [TestFixture]
    public class NRefactoryDeclerationExaminerTests
    {
        private readonly IExamineSourceCodeForDeclerations sourceExaminer = new NRefactoryDeclerationExaminer();
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
        public void ShouldTellMeAboutAnyClassDeclerationsWithinCode()
        {
            SetupCodeFile(@"public class Foo  {  } public class Bar  {  }");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.Count(), Is.EqualTo(2));
            Assert.That(types.HasADeclerationCalled("Foo"));
            Assert.That(types.HasADeclerationCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeAboutAnyEnumDeclerationsWithinCode()
        {
            SetupCodeFile(@"public enum Foo{First} public enum Bar{First}");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.Count(), Is.EqualTo(2));
            Assert.That(types.HasADeclerationCalled("Foo"));
            Assert.That(types.HasADeclerationCalled("Bar"));
        }

        [Test]
        public void ShouldTellMeAboutAnyInterfaceDeclerationsWithinCode()
        {
            SetupCodeFile(@"public interface IFoo{} public interface IBar{}");
            var types = sourceExaminer.ExamineSource(codeFile);
            Assert.That(types.Count(), Is.EqualTo(2));
            Assert.That(types.HasADeclerationCalled("IFoo"));
            Assert.That(types.HasADeclerationCalled("IBar"));
        }
        
    }
}