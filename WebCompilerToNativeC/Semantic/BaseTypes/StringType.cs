using System;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class StringType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}