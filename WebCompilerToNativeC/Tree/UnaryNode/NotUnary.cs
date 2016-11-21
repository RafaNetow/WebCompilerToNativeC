using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
   public class NotUnary :  BaseClass.UnaryNode
    {
        public override BaseType ValidateSemantic()
        {
            throw new System.NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
