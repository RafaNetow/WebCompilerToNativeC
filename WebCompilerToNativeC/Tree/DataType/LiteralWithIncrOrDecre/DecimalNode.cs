using System;
using System.Globalization;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.DataType.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre
{
    public class DecimalNode : LiteralWithOptionalIncrementOrDecrement
    {
        public float DecimalValue { get; set; }
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override void SetValue(string value)
        {
            DecimalValue = float.Parse(Value);
        }
    }
}
