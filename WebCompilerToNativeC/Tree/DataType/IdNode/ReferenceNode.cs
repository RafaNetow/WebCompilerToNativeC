using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree.DataType.IdNode
{
   public class ReferenceNode : AccesorNode
    {
        public IdNode Id { get; set; }

        public override BaseType Validate(BaseType type)
       {
           throw new NotImplementedException();
       }

       public override string GeneratedCodeAttribute()
       {
           throw new NotImplementedException();
       }
    }
}
