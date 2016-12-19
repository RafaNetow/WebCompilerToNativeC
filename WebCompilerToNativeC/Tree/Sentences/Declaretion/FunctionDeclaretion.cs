using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes.FuncType;

namespace WebCompilerToNativeC.Tree.Sentences.Declaretion
{
   public class FunctionDeclaretion : DeclarationNode
   {
      public List<DeclarationNode> ParameterList;
       public List<SentencesNode> ListOfEspecialSentences;
        public override void ValidateSemantic()
        {
            Context.StackOfContext.Stack.Push(new TypesTable());


            

            var typeOfReturn = Type.ValidateSemantic();


            foreach (var declarationNode in ParameterList)
            {
                declarationNode.ValidateSemantic();
            }
            foreach (var listOfEspecialSentence in ListOfEspecialSentences)
            {
                if (listOfEspecialSentence is ReturnNode)
                {
                    var returnSentences = (ReturnNode) listOfEspecialSentence;
                    if (!returnSentences.ValidateSemantic(typeOfReturn))
                        throw new SemanticException("El tipo de retorno tiene que ser igual al asignado a la funcion");
                }
                else
                {
                    listOfEspecialSentence.ValidateSemantic();
                }

            }
            Context.StackOfContext.Stack.Pop();
            Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value,new FunctionType() {ListOfParemterters = ParameterList,ReturnParam = typeOfReturn}, Variable.Accesors.Count);
            //Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value, baseType);

        }

        public override void Interpretation()
        {
            Context.StackOfContext.Stack.Push(Context.StackOfContext.RemembersContext[CodeGuid]);

            Context.StackOfContext.FunctionsNodes.Add(Variable.Value, this);

            //StackContext.Context.PastContexts.Remove(CodeGuid);
            Context.StackOfContext.Stack.Pop();
        }

        public Value Execute()
        {
            //  StackContext.Context.Stack.Push(StackContext.Context.PastContexts[CodeGuid]);
            dynamic returnValue = null;

            foreach (var sentence in ListOfEspecialSentences)
            {

                if (sentence is ReturnNode)
                {
                    returnValue = (sentence as ReturnNode).GetValueOfReturn();
                }
                sentence.Interpretation();
            }

            //     StackContext.Context.Stack.Pop();

            return returnValue;
        }

    }
}
