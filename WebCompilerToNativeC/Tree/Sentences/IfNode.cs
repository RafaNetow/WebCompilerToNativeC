using System;
using System.Collections.Generic;
using System.Reflection;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;
using SemanticException = WebCompilerToNativeC.Semantic.BaseTypes.SemanticException;

namespace WebCompilerToNativeC.Tree.Sentences
{
    public class IfNode : SentencesNode
    {
        public ExpressionNode IfCondition;
        public List<SentencesNode> TrueBlock = new List<SentencesNode>();
        public List<SentencesNode> FalseBlock = new List<SentencesNode>();

        public override void ValidateSemantic()
        {
            Context.StackOfContext.Stack.Push(new TypesTable());
            if (IfCondition.ValidateSemantic() is BooleanType)
            {


                foreach (var sentencesNode in TrueBlock)
                {




                    sentencesNode.ValidateSemantic();
                }
                foreach (var sentencesNode in FalseBlock)
                {
                    sentencesNode.ValidateSemantic();
                }


                Context.StackOfContext.RemembersContext.Add(CodeGuid, Context.StackOfContext.Stack.Pop());
            }
            else
            {
                throw new SemanticException(
                    $"Error Row:{IfCondition.NodePosition.Row}Col:{IfCondition.NodePosition.Column} La expresion dentro del if debe ser booleana");
            }

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

        public override void Interpretation()
        {
            Context.StackOfContext.Stack.Push(Context.StackOfContext.RemembersContext[CodeGuid]);
            dynamic condition = IfCondition.Interpretation();
            if (condition.Value)
            {
                if (TrueBlock == null) return;
                foreach (var sentencesNode in TrueBlock)
                {
                    if (sentencesNode is ContinueNode)
                    {
                        continue;
                    }

                    if (sentencesNode is BreakNode)
                    {
                        goto algo;
                    }

                    if (sentencesNode is ReturnNode)
                    {
                        return;
                    }
                    sentencesNode.Interpretation();


                }
            algo:
                Console.WriteLine("asdsad");
            }

            else
            {
               
                {
                    if (FalseBlock == null) return;
                    foreach (var sentencesNode in FalseBlock)
                    {
                        if (sentencesNode is ContinueNode)
                        {
                            continue;
                        }

                        if (sentencesNode is BreakNode)
                        {
                            break;
                        }

                        if (sentencesNode is ReturnNode)
                        {
                            return;
                        }
                        sentencesNode.Interpretation();


                    }


                    Context.StackOfContext.Stack.Pop();
                }
            }
        }
    }
}
