using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Tree
{
   public abstract class SentencesNode
   {
       public abstract void ValidateSemantic();
       public abstract string GenerateCode();
   }
}
