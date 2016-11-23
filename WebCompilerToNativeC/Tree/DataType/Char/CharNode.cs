using System;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree.DataType.Char
{
    public class CharNode : DataType.BaseClass.DataType
    {
        public char CharValue;


        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override void SetValue(string value)
        {
            CharValue = value[0];
        }
    }
}
