using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Tree.Sentences.Declaretion
{
   public class FunctionDeclaretion : DeclarationNode
   {
      public List<DeclarationNode> ParameterList;
       public List<SentencesNode> ListOfEspecialSentences;

   }
}
