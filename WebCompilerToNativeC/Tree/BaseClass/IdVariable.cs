using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.DataType;

namespace WebCompilerToNativeC.Tree.BaseClass
{
    public class IdVariable : ExpressionNode
    {
        public string Value;
        public UnaryNode IncrementOrDecrement;
        public List<AccesorNode> Accesors = new List<AccesorNode>();
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
