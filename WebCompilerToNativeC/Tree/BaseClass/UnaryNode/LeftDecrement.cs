using System;
using WebCompilerToNativeC.Semantic;

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
    }
}
