using System;
using WebCompilerToNativeC.interpretation.BaseClass;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    internal class ConstType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }

        public override Value GetDefaultValue()
        {
            throw new NotImplementedException();
        }
    }
}