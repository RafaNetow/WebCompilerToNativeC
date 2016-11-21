using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree.BinaryNode
{
   public class AndBinary : BaseClass.UnaryNode
    {
       public override BaseType ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
           return GetCode("&");
       }
    }
}
