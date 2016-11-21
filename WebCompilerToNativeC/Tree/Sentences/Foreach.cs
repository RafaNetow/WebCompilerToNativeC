using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.DataType.IdNode;

namespace WebCompilerToNativeC.Tree.Sentences
{
   public abstract class Foreach
    {
        public  IdNode DataType;
        public IdNode Element;
        public IdNode ListToForeach;
        public List<SentencesNode> Sentences;


    }
}
