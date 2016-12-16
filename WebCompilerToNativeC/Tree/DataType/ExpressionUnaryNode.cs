using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType
{
   public class ExpressionUnaryNode : ExpressionNode
   {
       public Tree.BaseClass.UnaryNode UnaryOperator { get; set; }
        public  ExpressionNode Factor { get; set; }
        public override BaseType ValidateSemantic()
        {
            return Factor.ValidateSemantic();
        }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }

       public override Value Interpretation()
       {
           return Factor.Interpretation();
       }
   }
}
