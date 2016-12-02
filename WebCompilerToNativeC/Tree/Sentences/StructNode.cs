using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Tree
{
   public  class StructNode : SentencesNode
    {
        public IdVariable NameOfStruct = new IdVariable();
        public  List<DeclarationNode>  StructItems = new List<DeclarationNode>();
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
