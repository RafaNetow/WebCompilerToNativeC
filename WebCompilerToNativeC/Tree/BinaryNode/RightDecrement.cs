using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree 
{
    public class RightDecrement : BaseClass.UnaryNode
 
{
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            return Value.GenerateCode() + "--";
        }
}
}
