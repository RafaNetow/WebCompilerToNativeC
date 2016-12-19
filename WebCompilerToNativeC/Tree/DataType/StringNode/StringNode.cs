using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public class StringNode : DataType.BaseClass.DataType
    {
        public string StringValue { get; set; }
        public override BaseType ValidateSemantic()
        {
            return Context.StackOfContext.GetType("string");
        }

        public override string GenerateCode()
        {
            return $"{Value}";
        }

        public override Value Interpretation()
        {
            return new StringValue() { Value = Value } ;
        }

        public override void SetValue(string value)
        {
            StringValue = value;
        }
    }
}
