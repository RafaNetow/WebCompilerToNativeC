using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value,new FunctionType() {ListOfParemterters = ParameterList,ReturnParam = typeOfReturn});
            //Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value, baseType);

        }

    }
}
