using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType
{
    public abstract class AccesorNode : ExpressionNode
    {
        public abstract BaseType ValidateSemantic(string variable);
    }
}
