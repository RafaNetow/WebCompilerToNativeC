using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
   public abstract class AccesorNode
    {
        public abstract BaseType Validate(BaseType type);
        public abstract string GeneratedCodeAttribute();
    }
}
