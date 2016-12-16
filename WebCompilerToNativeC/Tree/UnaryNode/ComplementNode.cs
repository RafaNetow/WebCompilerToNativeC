using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseClass.BaseTypes;

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

        public override Value Interpretation()
        {
            throw new NotImplementedException();
        }
}
}
