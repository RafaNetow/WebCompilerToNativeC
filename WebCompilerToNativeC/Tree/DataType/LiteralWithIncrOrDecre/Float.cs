using System;
using System.Globalization;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType
{
   public class Float : LiteralWithOptionalIncrementOrDecrement
    {
        public double Value { get; set; }
        public override BaseType ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
            return Value.ToString(CultureInfo.InvariantCulture);

        }
    }
}
