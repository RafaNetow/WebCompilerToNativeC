using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
   public class AndBinary : BaseClass.UnaryNode
    {
      

       public override string GenerateCode()
       {
           return GetCode("&");
       }
    }
}
