using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Lexer;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public abstract class ExpressionNode : ICloneable
    {
        public abstract BaseType ValidateSemantic();
        public abstract string GenerateCode();
        public abstract Value Interpretation();
        public Token NodePosition;
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
