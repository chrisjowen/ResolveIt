using NUnit.Framework;
using WellItCouldWork.TestRunner;

namespace WellItCouldWork.Tests.TestRunner
{
    [TestFixture]
    public class NunitTestRunnerTests
    {
        [Test]
        public void ShouldRunTestsFromAssembly()
        {
            var testRunner = new NUnitConsoleRunner("TestData/test-assembly.dll");
            var output = testRunner.RunAllTests();
            Assert.That(output.ToLower(), Contains.Substring("failed on purpose"));
        }
    }

  
}
