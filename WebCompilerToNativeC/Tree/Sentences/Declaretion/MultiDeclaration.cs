using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.DataType.IdNode;

namespace WebCompilerToNativeC.Tree.Sentences.Declaretion
{
   public  class MultiDeclaration : DeclarationNode
   {
     public  List<IdNode> ListOfIdNodes;
    
   }
}
