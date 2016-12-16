using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree.UnaryNode
{
   public class LeftDecrement : BaseClass.UnaryNode
    {
       public override BaseType ValidateSemantic()
       {
           return Value.ValidateSemantic();
       }

       public override string GenerateCode()
       {
           return GetCode("--");
       }

       public override Value Interpretation()
       {
           throw new NotImplementedException();
       }
    }
}
