using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public  abstract class BinaryOperator: ExpressionNode
    {
        public ExpressionNode RightOperand;
        public ExpressionNode LeftOperand;

        public string GetCode(string Operator)
        {
            return LeftOperand.GenerateCode() + Operator + RightOperand.GenerateCode();

        }
        

        
    }
}
