using System;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
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
