using System.Collections.Generic;
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
        }
    }
}
