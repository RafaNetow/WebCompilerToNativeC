using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Tree
{
   public class ContinueNode : SentencesNode
    {
       public override void ValidateSemantic()
       {
         
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }
    }
}
