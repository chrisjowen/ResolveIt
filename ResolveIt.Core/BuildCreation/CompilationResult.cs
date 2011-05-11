using System.Collections.Generic;
using ResolveIt.Core.SyntaxHelpers;

namespace ResolveIt.Core.BuildCreation
{
    public class CompilationResult
    {
        public IEnumerable<string> Errors { get; private set; }
        public string AssemblyLocation { get; private set; }

        public CompilationResult(IEnumerable<string> errors, string assemblyLocation)
        {
            Errors = errors;
            AssemblyLocation = assemblyLocation;
        }

        public bool HasErrors
        {
            get { return Errors.IsNotEmpty(); }
        }
    }
}