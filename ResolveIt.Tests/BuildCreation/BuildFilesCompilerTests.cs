using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using ResolveIt.Core.BuildCreation;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;

namespace ResolveIt.Tests.BuildCreation
{


    [TestFixture]
    public class BuildFilesCompilerTests
    {
        private ICodeFileInfo GetCodeFile(string fileName)
        {
            var filePath = string.Format(@"TestData\AssemblyBuilderData\{0}", fileName);
            var file = new FileInfo(filePath);
            return new CodeFileInfo(file.Name, file.DirectoryName);
        }
        private IReferenceInfo GetReference(string reference)
        {
            var filePath = string.Format(@"{0}", reference);
            var file = new FileInfo(filePath);
            return new ReferenceInfo(file.Name, file.DirectoryName);
        }

        [Test]
        public void ShouldBuildASingleEmptyClassWithoutError()
        {
            var emptyClass = GetCodeFile("EmptyClass.cs.txt");
            var buildFiles = new BuildCodeFiles(emptyClass, new List<IDependencyInfo>(), new List<IReferenceInfo>());
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(!compilationResult.HasErrors);
            Assert.That(!string.IsNullOrEmpty(compilationResult.AssemblyLocation));
        }

        [Test]
        public void ShouldFailToBuildAClassWithAnUnreferencedDependency()
        {
            var unreferencedClass = GetCodeFile("UnreferencedDependency.cs.txt");
            var buildFiles = new BuildCodeFiles(unreferencedClass, new List<IDependencyInfo>(), new List<IReferenceInfo>());
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(compilationResult.HasErrors);
        }

        [Test]
        public void ShouldFailToBuildAClassWithAValidReferenceIfReferenceLoactionNotSupplied()
        {
            var unreferencedClass = GetCodeFile("ReferencedDependency.cs.txt");
            var buildFiles = new BuildCodeFiles(unreferencedClass, new List<IDependencyInfo>(), new List<IReferenceInfo>());
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(compilationResult.HasErrors);
        }

        [Test]
        public void ShouldBuildAClassWithAValidReferenceIfReferenceLoactionIsSupplied()
        {
            var unreferencedClass = GetCodeFile("ReferencedDependency.cs.txt");
            var referenceInfo = GetReference("ICSharpCode.NRefactory");
            var buildFiles = new BuildCodeFiles(unreferencedClass, new List<IDependencyInfo>(), new List<IReferenceInfo> { referenceInfo });
            var compilationResult = new BuildFilesCompiler().Compile(buildFiles);
            Assert.That(compilationResult.HasErrors);

        }
    }

}
