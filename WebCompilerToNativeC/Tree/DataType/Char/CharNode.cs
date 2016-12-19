using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
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
               
            return Context.StackOfContext.GetType("char");
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override Value Interpretation()
        {

            return new CharValue() {Value =Value};
        }

        public override void SetValue(string value)
        {
            CharValue = value[0];
        }
    }
}
