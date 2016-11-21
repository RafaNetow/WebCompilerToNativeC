using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree.UnaryNode
{
    public class LeftIncrement : BaseClass.UnaryNode
    {
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            return GetCode("++");
        }
    }
}
