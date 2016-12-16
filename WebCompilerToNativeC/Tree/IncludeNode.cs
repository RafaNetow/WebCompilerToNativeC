using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Tree
{
    public class IncludeNode : SentencesNode
    {
        public string LibreryName;

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
