using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
    public class StringNode : ExpresionNode
    {
        public string Value { get; set; }
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            return $"{Value}";
        }
    }
}
