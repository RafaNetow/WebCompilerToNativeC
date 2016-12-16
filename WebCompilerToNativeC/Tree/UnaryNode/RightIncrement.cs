using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class RightIncrement  : BaseClass.UnaryNode
    {

        public override BaseType ValidateSemantic()
       {
            return Value.ValidateSemantic();
        }

       public override string GenerateCode()
       {
           return Value.GenerateCode() + "++";
       }

       public override Value Interpretation()
       {
           throw new NotImplementedException();
       }
    }
}
