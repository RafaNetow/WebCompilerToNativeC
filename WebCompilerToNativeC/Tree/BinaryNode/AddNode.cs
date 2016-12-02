using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
   public class AddNode : BinaryOperator
    {

       public AddNode ()
       {
           Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {
              
               
               
           };
       }

        public override string GenerateCode()
       {
           return GetCode("+");
       }
    }
}
