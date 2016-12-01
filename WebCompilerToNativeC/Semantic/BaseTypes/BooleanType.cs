using System;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    internal class BooleanType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}