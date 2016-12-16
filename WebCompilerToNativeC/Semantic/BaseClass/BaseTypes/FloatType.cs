using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    internal class FloatType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }

        public override Value GetDefaultValue()
        {
            return new FloatValue() {Value = 0};
        }
    }
}