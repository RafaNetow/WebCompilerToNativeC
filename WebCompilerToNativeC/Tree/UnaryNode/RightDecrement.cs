using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

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

        public override Value Interpretation()
        {
            throw new NotImplementedException();
        }
}
}
