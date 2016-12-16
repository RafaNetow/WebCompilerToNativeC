using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Tree.Sentences.Struct
{
    public class StructDeclaretion : SentencesNode

    {
        public string NameOfStruc;
        public string Nameriable;


        public override void ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override void Interpretation()
        {
            throw new NotImplementedException();
        }
    }
}
