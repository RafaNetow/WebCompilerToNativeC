using System;
using System.CodeDom;
using System.Collections.Generic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.DataType.IdNode;

namespace WebCompilerToNativeC.Semantic.BaseTypes.Struct
{
    public class StructType : BaseType
    {


        public List<StructParams> ListOfParams = new List<StructParams>();

        public StructType(List<StructParams> listOfParams)
        {
            if (listOfParams == null) throw new ArgumentNullException(nameof(listOfParams));
              this.ListOfParams =listOfParams;
        }

        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }

        public BaseType ContainMember(IdNode currenStructParam)
        {
            foreach (var structParam in ListOfParams)
            {
                if (structParam.Name == currenStructParam.Value)
                { 
                    var typeOfReturn = structParam.Type.ValidateSemantic();
                    typeOfReturn.LenghtOfProperties = structParam.LengOfProperties;




                    return structParam.Type.ValidateSemantic();
                    
                }
               
            }

            throw new SemanticException($"La propiedad  {currenStructParam.Value} no se encuentra en el estruct" );
        }
        

        

    }
}