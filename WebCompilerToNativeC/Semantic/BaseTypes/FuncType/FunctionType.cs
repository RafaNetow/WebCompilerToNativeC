using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
