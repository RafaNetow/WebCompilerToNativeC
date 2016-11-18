using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
    public  abstract class BinaryOperator: ExpresionNode
    {
        public ExpresionNode RightOperand;
        public ExpresionNode LeftOperand;

        public string GetCode(string Operator)
        {
            return LeftOperand.GenerateCode() + Operator + RightOperand.GenerateCode();

        }
        

        
    }
}
