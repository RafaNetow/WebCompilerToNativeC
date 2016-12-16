using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class DateType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }

        public override Value GetDefaultValue()
        {
           return new DateValue() {Value = DateTime.Now};
        }
    }
}