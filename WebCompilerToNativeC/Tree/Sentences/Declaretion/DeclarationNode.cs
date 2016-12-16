using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.DataType.IdNode;
using WebCompilerToNativeC.Tree.DataType.Struct;
using WebCompilerToNativeC.Tree.Sentences.Structs;

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
          var baseType=  (Type != null) ? Type.ValidateSemantic() : Context.StackOfContext.GetType(TypeStructOrEnum);
            
           if(baseType is StructType && Type == null)
                Type = new StructDataType() {Value = TypeStructOrEnum};
            if (Variable.ValueOfAssigment != null)
            {
                var baseTypeAssignment = Variable.ValueOfAssigment.ValidateSemantic();
                if (baseType != baseTypeAssignment)
                    throw new SemanticException($"La asignacion tiene que ser del mismo tipo");
            }
            Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value,baseType,Variable.Accesors.Count);
           
            Context.StackOfContext.Stack.Peek()
                .InformatioNVariable.Add(Variable.Value, new InforamtionVariable() {Lenght = Variable.Accesors.Count});

        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override void Interpretation()
        {
            var value = Variable.ValueOfAssigment.Interpretation();

            Context.StackOfContext.Stack.Peek().SetVariableValue(Variable.Value, Variable.ValueOfAssigment.Interpretation());
        }
    }
}
