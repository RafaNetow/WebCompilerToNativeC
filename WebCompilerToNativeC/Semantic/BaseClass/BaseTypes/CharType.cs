using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class CharType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return true;
        }

        public override Value GetDefaultValue()
        {
            return new CharValue() {Value = "0"};
        }
    }
}