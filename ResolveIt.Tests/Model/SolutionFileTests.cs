using NUnit.Framework;
using ResolveIt.Core.Model;

namespace ResolveIt.Tests.Model
{
    [TestFixture]
    public class SolutionFileTests
    {
        [Test]
        public void ShouldLoadSolutionInfAndRetrieveProperties()
        {
            const string solutionName = "Solution";
            const string path = @"C:\";

            var codeFile = new SolutionInfo(solutionName, path);
            Assert.That(codeFile.Name, Is.EqualTo(solutionName));
            Assert.That(codeFile.Path, Is.EqualTo(path));
        }
    }
}