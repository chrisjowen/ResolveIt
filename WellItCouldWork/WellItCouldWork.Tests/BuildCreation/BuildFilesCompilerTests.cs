using System.Collections.Generic;
using NUnit.Framework;
using WellItCouldWork.BuildCreation;

namespace WellItCouldWork.Tests.BuildCreation
{
    [TestFixture]  
    public class BuildFilesCompilerTests
    {
        [Test]
        public void ShouldBuildASingleEmptyClassWithoutError()
        {
            var emptyClass = Class.FromPath(GetClassPath("EmptyClass.cs.txt"));
            var buildFiles = new BuildFiles(emptyClass, new List<Class>(), new List<Reference>());
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(!compilationResult.HasErrors);
            Assert.That(!string.IsNullOrEmpty(compilationResult.AssemblyLocation));
        }

        [Test]
        public void ShouldFailToBuildAClassWithAnUnreferencedDependency()
        {
            var unreferencedClass = Class.FromPath(GetClassPath("UnreferencedDependency.cs.txt"));
            var buildFiles = new BuildFiles(unreferencedClass, new List<Class>(), new List<Reference>());
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(compilationResult.HasErrors);
        }

        [Test]
        public void ShouldFailToBuildAClassWithAValidReferenceIfReferenceLoactionNotSupplied()
        {
            var unreferencedClass = Class.FromPath(GetClassPath("ReferencedDependency.cs.txt"));
            var buildFiles = new BuildFiles(unreferencedClass, new List<Class>(), new List<Reference>());
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(compilationResult.HasErrors);
        }

        [Test]
        public void ShouldBuildAClassWithAValidReferenceIfReferenceLoactionIsSupplied()
        {
            var unreferencedClass = Class.FromPath(GetClassPath("ReferencedDependency.cs.txt"));
            var buildFiles = new BuildFiles(unreferencedClass, new List<Class>(), new List<Reference> { new Reference("ICSharpCode.NRefactory")});
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(!compilationResult.HasErrors);
        }

        private static string GetClassPath(string fileName)
        {
            return string.Format(@"TestData\AssemblyBuilderData\{0}", fileName);
        }
    }
}
