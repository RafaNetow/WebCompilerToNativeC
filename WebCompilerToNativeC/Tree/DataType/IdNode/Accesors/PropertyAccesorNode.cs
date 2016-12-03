using System;
using System.IO.IsolatedStorage;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using EnumType = WebCompilerToNativeC.Semantic.BaseClass.EnumType;

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
        
        public override BaseType ValidateSemantic(string variable)
        {
            var typeOfVaribel = TypesTable.Instance.GetType(variable);

            if (typeOfVaribel is StructType)
            {
              var  structVariable = (StructType)typeOfVaribel;
                if (structVariable.ContainMember(Id))
                    return Id.ValidateSemantic();



            }
            if (typeOfVaribel is EnumType)
            {
                typeOfVaribel = (EnumType)typeOfVaribel;


            }



            var idType = Id.ValidateSemantic();
            return idType;
         
        }
    }
}
