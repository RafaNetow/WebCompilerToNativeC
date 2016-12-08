using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;
using SemanticException = WebCompilerToNativeC.Semantic.BaseTypes.SemanticException;

namespace WebCompilerToNativeC.Tree.Sentences
{
   public class IfNode : SentencesNode
    {
        public ExpressionNode IfCondition;
        public List<SentencesNode> TrueBlock;
        public List<SentencesNode> FalseBlock = new List<SentencesNode>();
        public override void ValidateSemantic()
        {
            if (IfCondition.ValidateSemantic() is BooleanType)
            {
                Context.StackOfContext.Stack.Push(new TypesTable());

                foreach (var sentencesNode in TrueBlock)
                {
                     sentencesNode.ValidateSemantic();
                }
                foreach (var sentencesNode in FalseBlock)
                {
                    sentencesNode.ValidateSemantic();
                }
                Context.StackOfContext.Stack.Pop();


            }
            throw new SemanticException("La expresion dentro del if debe ser booleana");
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
