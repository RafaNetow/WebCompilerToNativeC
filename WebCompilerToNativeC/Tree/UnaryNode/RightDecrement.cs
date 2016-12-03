using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree 
{
    public class RightDecrement : BaseClass.UnaryNode
 
{
        public override BaseType ValidateSemantic()
        {
            return Value.ValidateSemantic();
        }

        public override string GenerateCode()
        {
            return Value.GenerateCode() + "--";
        }
}
}
