using System;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree
{
 public  class BreakNode : SentencesNode
   
{
     public override void ValidateSemantic()
     {
    
     }

     public override string GenerateCode()
     {
         throw new NotImplementedException();
     }

     public override void Interpretation()
     {
 
     }
}
}
