using WebCompilerToNativeC.Lexer;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public abstract class ExpressionNode
    {
        public abstract BaseType ValidateSemantic();
        public abstract string GenerateCode();
        public Token NodePosition;
    }
}
