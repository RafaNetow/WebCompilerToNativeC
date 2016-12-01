namespace WebCompilerToNativeC.Semantic.BaseClass
{
    internal class IntType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return otherType is IntType;
        }

       
    }
}