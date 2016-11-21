using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.DataType.IdNode;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Tree.Sentences.Enum
{
   public class EnumerationNode: SentencesNode
    {
        public IdNode NameOfEnum { get; set; }
        public  LinkedList<IdForDeclaration> ListEnum { get; set; }

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
