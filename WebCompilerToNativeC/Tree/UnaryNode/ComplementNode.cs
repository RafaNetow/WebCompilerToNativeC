using System;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public class ComplementNode : BaseClass.UnaryNode
   

{
        public override BaseType ValidateSemantic()
        {
           return new IntType();
        }

        public override string GenerateCode()
        {
            return GetCode("~");
        }
}
}
