using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;

namespace WebCompilerToNativeC.Tree
{
   public class NotUnary :  BaseClass.UnaryNode
    {
        public override BaseType ValidateSemantic()
        {
            return new BooleanType();
        }

        public override string GenerateCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
