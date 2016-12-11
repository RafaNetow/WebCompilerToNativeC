using System;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class BooleanType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}