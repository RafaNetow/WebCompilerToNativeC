using System;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
    internal class StructType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}