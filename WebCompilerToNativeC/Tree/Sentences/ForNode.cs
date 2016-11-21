using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences
{
   public class ForNode : SentencesNode
   {
       public ExpressionNode FirstCondition;
       public ExpressionNode SecondCondition;
       public ExpressionNode ThirdCondition;
       public List<SentencesNode> Sentences;
        
         
   

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
