using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.DataType.IdNode;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Tree.Sentences
{
    public class ConstNode : SentencesNode
    {
         public  IdNode Id;
        public IdNode DataType;
        public Assignation Expression;

        public List<PointerNode> ListOfPointers;


        public override void ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public  void SetValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
