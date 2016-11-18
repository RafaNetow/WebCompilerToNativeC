using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
    public abstract class ExpresionNode
    {
        public abstract BaseType ValidateSemantic();
        public abstract string GenerateCode();
    }
}
