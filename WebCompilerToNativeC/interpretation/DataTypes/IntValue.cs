using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;

namespace WebCompilerToNativeC.interpretation.DataTypes
{
   public  class IntValue : Value
   {
       public int Value;
        public override Value Clone()
       {
           return new IntValue() {Value = Value} ;
       }
    }
}
