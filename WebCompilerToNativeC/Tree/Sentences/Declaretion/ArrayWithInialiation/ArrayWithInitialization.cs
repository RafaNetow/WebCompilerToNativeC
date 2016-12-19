using System.Collections.Generic;
using System.Linq;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.DataType.IdNode;
using WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre;

namespace WebCompilerToNativeC.Tree.Sentences.Declaretion.ArrayWithInialiation
{
    public class ArrayWithInitialization : DeclarationNode

    {
        public List<ExpressionNode> InitValues = new List<ExpressionNode>();

        public override void ValidateSemantic()
        {

            var typeOfDeclaretion = Type.ValidateSemantic();
            foreach (var expressionNode in InitValues)

            {

              
                if (expressionNode.ValidateSemantic() != typeOfDeclaretion)
                {
                    throw new SemanticException($"Error: Row {expressionNode.NodePosition.Row} Colum:{expressionNode.NodePosition.Column} los elementos del arreglo deben ser igual que el tipo declarado");
                }
                
            }
            var variableClone = (IdVariable)Variable.Clone();
            Context.StackOfContext.Stack.Peek().RegisterType(Variable.Value, Type.ValidateSemantic(), variableClone.Accesors?.Count ?? 0);
            Context.StackOfContext.Stack.Peek().InformatioNVariable.Add(Variable.Value, new InforamtionVariable() { Lenght = Variable.Accesors?.Count ?? 0 });
            Context.StackOfContext.Stack.Peek().ValuesOfArrays.Add(Variable.Value, new List<Value>());
        }
        public override void Interpretation()
        {

            var accesorsOfArray =
           Context.StackOfContext.Stack.Peek().InformatioNVariable[Variable.Value];

            int? row = null;
            int? column = null;


            if (accesorsOfArray.Lenght == 1)
            {
                dynamic val = Variable.Accesors.First().Interpretation();
                row = val.Value;
            }
            else
            {
                dynamic val = Variable.Accesors.First().Interpretation();
                dynamic val2 = Variable.Accesors.Last().Interpretation();
                row = val.Value;
                column = val2.Value;
            }
            var values = new List<Value>();

            int pos1 = 0;
            int pos2 = 0;

            foreach (var node in InitValues)
            {
                dynamic value = node.Interpretation();

                if (column == null)
                {
                    value.row = pos1;
                }

                if (column != null)
                {
                    value.row = pos1;
                    value.column = pos2;
                }

                values.Add(value);

                if (column != null)
                {
                    if (pos2 < column.Value - 1)
                    {
                        pos2++;
                    }
                    else
                    {
                        pos1++;
                        pos2 = 0;
                    }
                }
                else
                {
                    if (pos1 < row.Value)
                    {
                        pos1++;
                    }
                }

            }

            Context.StackOfContext.Stack.Peek().SetArrayVariableValue(Variable.Value, values);
        }
    }
}
