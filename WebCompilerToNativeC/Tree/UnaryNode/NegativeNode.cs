using System;
using WebCompilerToNativeC.Semantic;


namespace WebCompilerToNativeC.Tree
{
   public class NegativeNode : BaseClass.UnaryNode
    {
       public override BaseType ValidateSemantic()
       {
           return Value.ValidateSemantic();
       }

       public override string GenerateCode()
       {
           return GetCode("-");
       }
    }
}
