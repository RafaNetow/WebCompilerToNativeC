using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences
{
   public class IfNode : SentencesNode
    {
        public ExpressionNode IfCondition;
        public List<SentencesNode> TrueBlock;
        public List<SentencesNode> FalseBlock;
        public override void ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
            string falseBlock = "";
            string trueBlock = "";
            foreach (var sentencesNode in FalseBlock)
            {
                falseBlock = falseBlock + sentencesNode.GenerateCode();
            }
            foreach (var sentences in TrueBlock)
            {
                trueBlock = trueBlock + sentences.GenerateCode();
            }

            return "if (" + IfCondition.GenerateCode() + ")" + "{" + trueBlock + "}" + "else" + "{" + falseBlock + "}";
        }
    }
}
