using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences.Fors
{
   public class ForNode : Tree.Fors
   {
       public IdVariable FirstCondition;
       public ExpressionNode SecondCondition;
       public ExpressionNode ThirdCondition;
 
        
         
   

       public override void ValidateSemantic()
       {

           FirstCondition.ValidateSemantic();

            if(!(SecondCondition.ValidateSemantic() is BooleanType))
                throw new SemanticException("Se espera una expression booleana");

           ThirdCondition.ValidateSemantic();

            foreach (var statement in ListStencnesNode)
           {
               statement.ValidateSemantic();
           }
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }

       public override void Interpretation()
       {
           throw new NotImplementedException();
       }
   }
}
