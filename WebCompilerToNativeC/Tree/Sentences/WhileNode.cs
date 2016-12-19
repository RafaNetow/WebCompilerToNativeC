using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.Sentences.Fors;
using SemanticException = WebCompilerToNativeC.Semantic.BaseTypes.SemanticException;

namespace WebCompilerToNativeC.Tree.Sentences
{
   public class WhileNode : SentencesNode
   {
       public ExpressionNode WhileCondition;
       public List<SentencesNode> Sentences;


        public override void ValidateSemantic()
        {
            Context.StackOfContext.Stack.Push(new TypesTable());
            if (!(WhileCondition.ValidateSemantic() is BooleanType))
                throw new SemanticException("Se esperaba expresion booleana en la sentencia while");

            foreach (var statement in Sentences)
            {
                statement.ValidateSemantic();
            }
            Context.StackOfContext.RemembersContext.Add(CodeGuid, Context.StackOfContext.Stack.Pop());
        }
       

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }

       public override void Interpretation()
       {
           Context.StackOfContext.Stack.Push(Context.StackOfContext.RemembersContext[CodeGuid]);

            dynamic conditional = WhileCondition.Interpretation();

          
            while (conditional.Value)
            {
                foreach (var statement in Sentences)
                {
                    statement.Interpretation();

                    if (statement is ContinueNode)
                    {
                        continue;
                    }

                    if (statement is BreakNode)
                    {
                        break;
                    }

                    if (statement is ReturnNode)
                    {
                        return;
                    }
                }

                conditional = WhileCondition.Interpretation();
            }

            Context.StackOfContext.Stack.Pop();
        }
   }
}
