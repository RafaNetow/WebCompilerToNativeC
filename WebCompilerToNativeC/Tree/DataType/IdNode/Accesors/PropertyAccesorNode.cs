using System;
using System.CodeDom;
using System.IO.IsolatedStorage;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;


namespace WebCompilerToNativeC.Tree.DataType.IdNode.Accesors
{
    public class PropertyAccesorNode : AccesorNode
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
           

            if (variable is StructType)
            {
              var  structVariable = (StructType)variable;
  
                return structVariable.ContainMember(Id);
                
            }
            if (variable is EnumType)
            {
                variable = (EnumType)variable;


            }



            var idType = Id.ValidateSemantic();
            return idType;
         
        }
    }
}
