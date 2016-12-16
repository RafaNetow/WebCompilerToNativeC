using System;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class SemanticException : Exception
    {
        public SemanticException(string message) : base(message)
        {


        }
    }
}