using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
    public class IdNode : ExpresionNode
    {
        public  string Value { get; set; }
        public List<AccesorNode> Accesors = new List<AccesorNode>();

        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            if (Accesors.Count == 0)
                return $"{Value}";
            var accesors = Accesors.Aggregate("", (current, accesorNode) => current + accesorNode.GeneratedCodeAttribute());
            return this.Value + accesors;

        }
    }

  
}
