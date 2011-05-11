using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;
using ResolveIt.Core.Investigation;
using ResolveIt.Core.Model;
using Rhino.Mocks;

namespace ResolveIt.Tests.Investigation
{
    [TestFixture]
    public class CodeFileLoaderTests
    {
        private ILoadCodeFileInfo codeFileLoader;
        private IExamineSourceCodeForDeclerations declerationExaminer;
        const string PathToCodeFile = @"TestData\WellItCouldWork\RootPathClass.cs";


        [SetUp]
        public void BeforeEachTest()
        {
            declerationExaminer = MockRepository.GenerateStub<IExamineSourceCodeForDeclerations>();
            codeFileLoader = new CodeFileLoader(declerationExaminer);
        }

        [Test]
        public void ShouldLoadCodeFileInfo()
        {
            var file = new FileInfo(PathToCodeFile);
            var dependencies = new List<IDeclerationInfo>
            {
                new DeclerationInfo(null, null)
            };

            declerationExaminer
                .Stub(examiner => examiner.ExamineSource(Arg<CodeFileInfo>.Is.Anything))
                .Return(dependencies);
            
            var codeFileInfo = codeFileLoader.FromFile(file);
            Assert.That(codeFileInfo.Name, Is.EqualTo("RootPathClass.cs"));
            Assert.That(codeFileInfo.Path, Is.EqualTo(@"c:\projects\ResolveIt\ResolveIt.Tests\bin\Debug\TestData\WellItCouldWork"));
            Assert.That(codeFileInfo.Declerations.Count(), Is.EqualTo(1));
            Assert.That(codeFileInfo.Declerations.First(), Is.EqualTo(dependencies.First()));
        }
    }


}