using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public class StringNode : DataType.BaseClass.DataType
    {
        public string StringValue { get; set; }
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            return $"{Value}";
        }

        public override void SetValue(string value)
        {
            StringValue = value;
        }
    }
}
