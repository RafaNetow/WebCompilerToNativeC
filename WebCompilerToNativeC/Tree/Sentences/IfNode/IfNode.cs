using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.Sentences.IfNode
{
   public class IfNode : SentencesNode
    {
        public ExpressionNode IfCondition;
        public List<SentencesNode> TrueBlock;
        public List<SentencesNode> FalseBlock = new List<SentencesNode>();
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
