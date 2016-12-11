using System.Collections.Generic;
using WebCompilerToNativeC.Tree.DataType.IdNode;

namespace WebCompilerToNativeC.Tree.Sentences.Fors
{
   public  class Foreach : Tree.Fors
    {
        public  IdNode DataType;
        public IdNode Element;
        public IdNode ListToForeach;


       public override void ValidateSemantic()
       {
           foreach (var sentencesNode in ListStencnesNode)
           {
               sentencesNode.ValidateSemantic();
               
           }
       }

       public override string GenerateCode()
       {
           throw new System.NotImplementedException();
       }
    }
}
