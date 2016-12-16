using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.IdNode
{
   public class ArrayAccesorNode : AccesorNode
   {
       public ExpressionNode Value;

       public override BaseType ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
           throw new NotImplementedException();
       }

       public override Value Interpretation()
       {
           throw new NotImplementedException();
       }


       public override BaseType ValidateSemantic(BaseType type)
       {
           int lengthOfProperties = type.LenghtOfProperties;
           var idType = Value.ValidateSemantic();
           if(!(lengthOfProperties > 0))
               throw  new SemanticException($"Error Col : {NodePosition.Column} Row : {NodePosition.Row} se han puesto mas accresore");
           type.LenghtOfProperties = lengthOfProperties;
           type.LenghtOfProperties--;       
           if (idType.BaseTypeEquivalent(idType, Context.StackOfContext.GetType("int"))) 
               return type;
           else
               throw new SemanticException($"Error Col: {NodePosition.Column} Row:{NodePosition.Row} Tiene que ingresar un accesor con un tipo correcto");
       }
   }
}
