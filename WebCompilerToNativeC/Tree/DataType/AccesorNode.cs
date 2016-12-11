using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType
{
    public abstract class AccesorNode : ExpressionNode
    {
        public abstract BaseType ValidateSemantic(BaseType variable);
    }
}
