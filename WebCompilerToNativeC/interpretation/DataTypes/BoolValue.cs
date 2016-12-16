using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;

namespace WebCompilerToNativeC.interpretation.DataTypes
{
    public class BoolValue : Value
    {
        public bool Value;
        public override Value Clone()
        {
            return new BoolValue() {Value = Value};
        }
    }
}
