using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Tree.DataType.IdNode;

namespace WebCompilerToNativeC.Semantic.BaseTypes.Struct
{
    public class StructType : BaseType
    {


        public List<StructParams> ListOfParams = new List<StructParams>();

        public StructType(List<StructParams> listOfParams)
        {
            if (listOfParams == null) throw new ArgumentNullException(nameof(listOfParams));
            listOfParams = this.ListOfParams;
        }

        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }

        public bool ContainMember(IdNode currenStructParam)
        {
            foreach (var structParam in ListOfParams)
            {
                if (structParam.Name == currenStructParam.Value)
                {
                    if (structParam.LengOfProperties == currenStructParam.Accesors.Count)
                    {
                        currenStructParam.ValidateSemantic();
                    }
                }
            }

            return false;
        }
        

        

    }
}