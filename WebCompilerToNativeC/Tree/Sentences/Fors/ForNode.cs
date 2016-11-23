using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences.Fors
{
   public class ForNode : Tree.Fors
   {
       public ExpressionNode FirstCondition;
       public ExpressionNode SecondCondition;
       public ExpressionNode ThirdCondition;
 
        
         
   

       public override void ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }
    }
}
