using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree.BaseClass
{
    public abstract class ExpressionNode
    {
        public abstract BaseType ValidateSemantic();
        public abstract string GenerateCode();
    }
}
