using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.DataType.IdNode;

namespace WebCompilerToNativeC.Tree.Sentences.Declaretion
{


    public class DeclarationNode : SentencesNode
    {
        public DataType.BaseClass.DataType Type;
        public IdVariable Variable;
        public List<PointerNode> ListOfPointers;
        public string TypeStructOrEnum;

        public override void ValidateSemantic()
        {
          var baseType=  Type.ValidateSemantic();
            if (Variable.ValueOfAssigment != null)
            {
                var baseTypeAssignment = Variable.ValueOfAssigment.ValidateSemantic();
                if (baseType != baseTypeAssignment)
                    throw new SemanticException("La asignacion tiene que ser del mismo tipo");
            }
            Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value,baseType,Variable.Accesors.Count);
        
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }
}
