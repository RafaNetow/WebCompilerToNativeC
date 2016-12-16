using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;

namespace WebCompilerToNativeC.Semantic.BaseClass.BaseTypes
{
    internal class IntType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is IntType;
        }

        public override Value GetDefaultValue()
        {
            return new IntValue() {Value = 0};
        }
    }
}