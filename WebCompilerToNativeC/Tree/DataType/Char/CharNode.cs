using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Tree
{
    public class CharNode : DataType.BaseClass.DataType
    {
        public char CharValue;
        public override void ValidateSemantic()
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
