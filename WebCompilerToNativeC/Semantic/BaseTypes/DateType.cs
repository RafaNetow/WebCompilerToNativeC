using System;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class DateType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }
    }
}