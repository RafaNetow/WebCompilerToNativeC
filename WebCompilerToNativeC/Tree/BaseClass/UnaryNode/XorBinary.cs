using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree. UnaryNode
{
   public class XorBinary : BaseClass.UnaryNode
    {
       public override BaseType ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
          return  GetCode("^");
       }
    }
}
