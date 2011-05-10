using NUnit.Framework;
using ResolveIt.Core.Model;

namespace ResolveIt.Tests.Model
{
    [TestFixture]
    public class ClassDefenitionTests
    {
        [Test]
        public void ShouldRetrieveClassProperties()
        {
            var codeFile = new CodeFileInfo(string.Empty, string.Empty);
            const string className = "Class";
            var aClass = new DependencyInfo(className, codeFile);
            Assert.That(aClass.Name, Is.EqualTo(className));
            Assert.That(aClass.CodeFileInfo, Is.EqualTo(codeFile));
        }
    }
}
