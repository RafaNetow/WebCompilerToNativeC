using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

       

       public override BaseType ValidateSemantic(BaseType type)
       {
           var idType = Value.ValidateSemantic();
           if(!(type.LenghtOfProperties>0))
               throw  new SemanticException("This  doesnt support more ArrayProperties");

           type.LenghtOfProperties--;       
           if (idType.BaseTypeEquivalent(idType, Context.StackOfContext.GetType("int"))) 
               return type;
           else
               throw new SemanticException("Tiene que ingresar un accesor con un tipo correcto");
       }
   }
}
