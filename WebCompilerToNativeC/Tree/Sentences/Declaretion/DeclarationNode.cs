using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WebCompilerToNativeC.interpretation.Interpretation;
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
              
                if (!TypesValidation.EquivalenceOfType(baseType, baseTypeAssignment))
                    throw new SemanticException($"Error Row:{Variable.NodePosition.Row} Column{Variable.NodePosition.Column} Asignacion no valida");
            }
            Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value,baseType,Variable.Accesors?.Count ?? 0);
           
            Context.StackOfContext.Stack.Peek()
                .InformatioNVariable.Add(Variable.Value, new InforamtionVariable() {Lenght = Variable.Accesors?.Count ?? 0 });

        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override void Interpretation()
        {
            var baseType = (Type != null) ? Type.ValidateSemantic() : Context.StackOfContext.GetType(TypeStructOrEnum);
            

            Context.StackOfContext.Stack.Peek().SetVariableValue(Variable.Value, (Variable.ValueOfAssigment == null) ? baseType.GetDefaultValue() : Variable.ValueOfAssigment.Interpretation());
        }
    }
}
