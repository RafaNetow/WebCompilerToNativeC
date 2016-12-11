using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.Struct
{
   public class StructDataType :  DateNode
    {
       public override BaseType ValidateSemantic()
       {
           return Context.StackOfContext.GetType(Value);

       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }
    }
}
