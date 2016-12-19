using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using SemanticException = WebCompilerToNativeC.Semantic.BaseTypes.SemanticException;

namespace WebCompilerToNativeC.Tree.Sentences.Fors
{
   public class ForNode : Tree.Fors
   {
       public IdVariable FirstCondition;
       public ExpressionNode SecondCondition;
       public ExpressionNode ThirdCondition;
 
        
         
   

       public override void ValidateSemantic()
       {
            Context.StackOfContext.Stack.Push(new TypesTable());
            FirstCondition.ValidateSemantic();

            if(!(SecondCondition.ValidateSemantic() is BooleanType))
                throw new SemanticException("Se espera una expression booleana");

           ThirdCondition.ValidateSemantic();

            foreach (var statement in ListStencnesNode)
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

            FirstCondition.Interpretation();
            //SecondCondition.Interpret();
           dynamic conditional1 = FirstCondition.Interpretation();
            dynamic conditional2 = SecondCondition.Interpretation();
            //dynamic conditional3 = ThirdCondition.Interpret();

          

            for (int i = conditional1.Value; conditional2.Value; ThirdCondition.Interpretation())
            {
                conditional2 = SecondCondition.Interpretation();

                if (!conditional2.Value) continue;

                foreach (var statement in ListStencnesNode)
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
            }

            Context.StackOfContext.Stack.Pop();
        }
   }
}
