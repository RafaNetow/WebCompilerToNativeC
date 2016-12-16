using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;

namespace WebCompilerToNativeC.Semantic.BaseTypes.FuncType
{
    class FunctionType : BaseType
    {
        public List<DeclarationNode> ListOfParemterters;
        public BaseType ReturnParam;
        public override bool IsAssignable(BaseType otherType)
        {
            throw new NotImplementedException();
        }

        public override Value GetDefaultValue()
        {
               return  new BoolValue();
        }
    }
}
