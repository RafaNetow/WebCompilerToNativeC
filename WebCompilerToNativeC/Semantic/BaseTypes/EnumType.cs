using System;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class EnumType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}