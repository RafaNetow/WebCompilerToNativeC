﻿using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
    public class ComplementNode : BaseClass.UnaryNode
   

{
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            return GetCode("~");
        }
}
}
