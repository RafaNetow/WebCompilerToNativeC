using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public class AndNode : BinaryOperator
    {
        public override object GetTypeValue(object right, object left, dynamic value)
        {
            throw new NotImplementedException();
        }

        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            return GetCode("&&"); 
        }

        public override Value Interpretation()
        {
            throw new NotImplementedException();
        }
    }

    class AndNodeImpl : AndNode
    {
    }
}
