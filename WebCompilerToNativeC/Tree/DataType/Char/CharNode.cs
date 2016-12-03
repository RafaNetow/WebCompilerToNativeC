using System;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;

namespace WebCompilerToNativeC.Tree.DataType.Char
{
    public class CharNode : DataType.BaseClass.DataType
    {
        public char CharValue;


        public override BaseType ValidateSemantic()
        {
            return new CharType();
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
