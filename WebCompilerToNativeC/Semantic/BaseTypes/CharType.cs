namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    internal class CharType : BaseType
    {
        public override bool IsAssignable(BaseType otherType)
        {
            return true;
        }
    }
}