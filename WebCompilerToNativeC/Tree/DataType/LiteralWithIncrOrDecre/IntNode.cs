using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.DataType.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre
{
    public class IntNode :LiteralWithOptionalIncrementOrDecrement
    {
        public int IntValue { get; set; }
        public override BaseType ValidateSemantic()
        {
            return Context.StackOfContext.GetType("int");
        }

        public override string GenerateCode()
        {
            return $"{IntValue}";
        }

        public override Value Interpretation()
        {
            return new IntValue() {Value =  Convert.ToInt32(Value)};
        }

        public override void SetValue(string value)
        {
            IntValue = int.Parse(value);
        }
    }
}
