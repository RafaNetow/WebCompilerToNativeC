using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.IdNode
{
   public class ReferenceNode : AccesorNode
    {
        public IdNode Id { get; set; }

       
       public override BaseType ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }

       public override BaseType ValidateSemantic(BaseType variable)
       {
           throw new NotImplementedException();
       }
    }
}
