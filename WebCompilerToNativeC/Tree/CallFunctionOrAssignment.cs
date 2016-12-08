using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public  class CallFunctionOrAssignment : SentencesNode
   {
      public IdVariable IdToAssignment;
       public ExpressionNode Expression;
       public override void ValidateSemantic()
       {
           Expression.ValidateSemantic();
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }
   }
}
