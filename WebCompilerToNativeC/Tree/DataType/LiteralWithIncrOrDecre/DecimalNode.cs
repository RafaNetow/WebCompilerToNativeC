using System;
using System.Globalization;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.DataType.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre
{
    public class DecimalNode : LiteralWithOptionalIncrementOrDecrement
    {
        public float DecimalValue { get; set; }
        public override BaseType ValidateSemantic()
        {
            return Context.StackOfContext.GetType("float");
        }

        public override string GenerateCode()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override Value Interpretation()
        {
            throw new NotImplementedException();
        }

        public override void SetValue(string value)
        {
            DecimalValue = float.Parse(Value);
        }
    }
}
