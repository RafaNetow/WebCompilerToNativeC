using System;
using System.Collections.Generic;
using System.Linq;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Tree.DataType.IdNode
{
    public class IdNode : ExpressionNode
    {
        public  string Value { get; set; }
        public List<AccesorNode> Accesors = new List<AccesorNode>();
        public List<PointerNode> ListOfPointer;
        public Assignation Assignation;
       

        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            if (Accesors.Count == 0)
                return $"{Value}";
            var accesors = Accesors.Aggregate("", (current, accesorNode) => current + accesorNode.GenerateCode());
            return this.Value + accesors;

        }
    }

  
}
