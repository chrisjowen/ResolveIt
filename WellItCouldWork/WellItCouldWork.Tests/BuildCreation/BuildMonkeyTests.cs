using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using WellItCouldWork.BuildCreation;
using WellItCouldWork.Investigation;
using WellItCouldWork.Tests.TestSyntaxHelpers;

namespace WellItCouldWork.Tests.BuildCreation
{
    [TestFixture]
    public class BuildMonkeyTests
    {
        private IExamineSourceFiles examiner;
        private ISolutionFile solutionFile;
        private IGetSourceCode sourceCodeRepo;

        [SetUp]
        public void BeforeEachTest()
        {
            examiner = MockRepository.GenerateStub<IExamineSourceFiles>();
            solutionFile = MockRepository.GenerateStub<ISolutionFile>();
            sourceCodeRepo = MockRepository.GenerateStub<IGetSourceCode>();
        }

        [Test]
        public void ShouldRetrieveClassDependenciesFromExaminer()
        {
            const string source = @"public class Foo() {}";
            var fooClass = new Class("Foo");

            sourceCodeRepo.Stub(sr => sr.SourceFor(fooClass)).Return(source);

            BuildMonkey
                .Using(examiner, solutionFile, sourceCodeRepo)
                .WhatFilesAreRequiredToBuild(fooClass);

            examiner.AssertWasCalled(e => e.ExamineSource((source)));
        }        
        
        [Test]
        public void ShouldNotAddDependencyIfNotInSolution()
        {
            examiner.Stub(e => e.ExamineSource(Arg<string>.Is.Anything)).Return(new List<TypeInfo> { "Foo" });
            solutionFile.Stub(s => s.FindClassByType(Arg<TypeInfo>.Is.Anything)).Return(new Class("stub"));
            
            var buildFiles = BuildMonkey
                                .Using(examiner, solutionFile, sourceCodeRepo)
                                .WhatFilesAreRequiredToBuild(string.Empty);

            Assert.That(buildFiles.HasAClassCalled("Foo.cs"), Is.False);
        }

        [Test]
        public void ShouldAddDependencyIfInSolution()
        {
            var fooClass = new Class("Foo.cs");
            TypeInfo type = "Foo";

            examiner.Stub(e => e.ExamineSource(Arg<string>.Is.Anything)).Return(new List<TypeInfo> { type });
            solutionFile.Stub(s => s.FindClassByType(type)).Return(fooClass);

            var buildFiles = BuildMonkey
                                .Using(examiner, solutionFile, sourceCodeRepo)
                                .WhatFilesAreRequiredToBuild(string.Empty);

            Assert.That(buildFiles.HasAClassCalled("Foo.cs"));

        }
 
        [Test]
        public void ShouldAddAllReferencesFoundInASolution()
        {
            var references = new List<Reference> { new Reference("System.Web") };
            solutionFile.Stub(s => s.AllReferences).Return(references);

            var buildFile = BuildMonkey
                                .Using(examiner, solutionFile, sourceCodeRepo)
                                .WhatFilesAreRequiredToBuild(string.Empty);

            Assert.That(buildFile.HasAReferenceCalled("System.Web.dll"));
        }
    }    
    
}


