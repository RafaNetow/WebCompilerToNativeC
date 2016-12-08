using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.Boolean
{
   public class BooleanNode : BaseClass.DataType
   {
      public bool BoolValue;
        

        public override BaseType ValidateSemantic()
       {
            return Context.StackOfContext.GetType("bool");
        }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }

       public override void SetValue(string value)
       {

           switch (value)
           {
                case "True":
                   BoolValue = true;
                   break;
                case "False":
                   BoolValue = false;
                   break;
           }
       }
   }
}
