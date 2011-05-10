using NUnit.Framework;

namespace WellItCouldWork.Tests
{
    [TestFixture]
    public class SimpleTestForConsoleRunner
    {
        [Test]
        public void ShouldJustPass()
        {
            Assert.IsTrue(true); 
        }

        [Test]
        public void ShouldUseExternalDependencyToPass()
        {
            const string className = "ClassName";
            var aClass = new Class(className);
            Assert.That(aClass.ClassName, Is.EqualTo(className)); 
        }
    }
}
