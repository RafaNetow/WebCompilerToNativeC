using System;
using System.Collections.Generic;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.DataType.IdNode;

namespace WebCompilerToNativeC.Semantic.BaseTypes
{
    public class EnumType : BaseType
    {
       public List<IdNode> EnumDeclaretion; 
        public EnumType(List<IdNode> enumDeclaretion1)
        {
            EnumDeclaretion = enumDeclaretion1;
        }

        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }

        public override Value GetDefaultValue()
        {
              return new BoolValue() ;
        }
    }
}