using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Lexer;

namespace WebCompilerToNativeC.Tree
{
   public abstract class SentencesNode : ICloneable
   {
       public abstract void ValidateSemantic();
       public abstract string GenerateCode();
       public abstract void Interpretation();
       public Token SentencesPosition;
        public Guid CodeGuid = Guid.NewGuid();
        public object Clone()
       {
          return this.MemberwiseClone();
       }
   }
}
