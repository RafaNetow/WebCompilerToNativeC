using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences.Case
{
    public class CaseStatement : SentencesNode
    {
        public ExpressionNode CaseOption;
        public  List<SentencesNode> Sentences;

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
