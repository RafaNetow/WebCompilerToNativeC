using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Lexer;

namespace WebCompilerToNativeC.Tree
{
   public abstract class SentencesNode
   {
       public abstract void ValidateSemantic();
       public abstract string GenerateCode();
       public Token SentencesPosition;
   }
}
