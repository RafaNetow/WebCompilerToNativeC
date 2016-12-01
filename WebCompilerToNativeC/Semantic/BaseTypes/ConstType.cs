using System;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    internal class ConstType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}