using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;
using SemanticException = WebCompilerToNativeC.Semantic.BaseTypes.SemanticException;

namespace WebCompilerToNativeC.Tree
{
    public  abstract class BinaryOperator: ExpressionNode
    {
        public ExpressionNode RightOperand;
        public ExpressionNode LeftOperand;
        public Dictionary<Tuple<BaseType, BaseType>, BaseType> Validation;

        public string GetCode(string Operator)
        {
            return LeftOperand.GenerateCode() + Operator + RightOperand.GenerateCode();

        }

        public override BaseType ValidateSemantic()
        {
            var leftType = LeftOperand.ValidateSemantic();
            var rightType = RightOperand.ValidateSemantic();

            BaseType result;
            if (Validation.TryGetValue(new Tuple<BaseType, BaseType>(leftType, rightType), out result))
            // Returns true.
            {
                return result;

            }
            else
            {
                throw new SemanticException("No se puede realizar esta operacion");
            }

        }

    }
}
