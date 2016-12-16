using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes.FuncType;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.IdNode
{
    public class CallFunction: ExpressionNode
    {
        public string NameOfFunction;
        public List<ExpressionNode> ListOfExpression; 

        public override BaseType ValidateSemantic()
        {

            var baseType = Context.StackOfContext.Stack.Peek().GetType(NameOfFunction);
            var functionType = (FunctionType) baseType;


            for (int i = 0; i < ListOfExpression.Count; i++)
            {
               if(ListOfExpression[i].ValidateSemantic() != functionType.ListOfParemterters[i].Type.ValidateSemantic() ) 
                    throw new SemanticException($"Error en la col: {functionType.ListOfParemterters[i].SentencesPosition.Column}row: {functionType.ListOfParemterters[i].SentencesPosition.Row}El parametro"+functionType.ListOfParemterters[i].Variable.Value+"no es del tipo correspondiente a la funcion ");

            }

            return functionType.ReturnParam;









        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override Value Interpretation()
        {
            throw new NotImplementedException();
        }
    }
}
