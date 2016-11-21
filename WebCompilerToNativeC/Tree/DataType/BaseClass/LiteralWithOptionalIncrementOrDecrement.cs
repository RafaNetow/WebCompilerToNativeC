using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.BaseClass
{
   public abstract class LiteralWithOptionalIncrementOrDecrement : DataType
   {
       public UnaryNode DecremmentOrIncremment;
   }
}
