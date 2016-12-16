using System;
using System.Collections.Generic;
using System.Linq;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;

namespace WebCompilerToNativeC.Tree
{
    public class IdVariable : ExpressionNode
    {
        
        public string Value;
        public BaseClass.UnaryNode IncrementOrDecrement; 
        public ExpressionNode TypeOfAssignment;
        public List<AccesorNode> Accesors;
        public ExpressionNode ValueOfAssigment;


        public IdVariable()
        {
            
        }


        public IdVariable(ExpressionNode valueOfAssigment )
        {
            this.ValueOfAssigment = valueOfAssigment;
        }
        public IdVariable(string value, BaseClass.UnaryNode incrementOrDescrement, ExpressionNode typeOfAssignment,
            List<AccesorNode> accesors, ExpressionNode valueOfAssigment)
        {
            this.Value = value;
            this.IncrementOrDecrement = incrementOrDescrement;
            this.TypeOfAssignment = typeOfAssignment;
            this.Accesors = accesors;
            this.ValueOfAssigment = valueOfAssigment;

        }

        public override BaseType ValidateSemantic()
        {
            if (ValueOfAssigment != null)
            {
             var baseTypeAssigment =    ValueOfAssigment.ValidateSemantic();
             var baseTypeOfVariable =   Context.StackOfContext.Stack.Peek().GetType(Value);
                
                foreach (var accesorNode in Accesors)
                {
                    baseTypeOfVariable = accesorNode.ValidateSemantic(baseTypeOfVariable);

                }
                if (!(baseTypeOfVariable.LenghtOfProperties == 0))
                    throw new SemanticException($"Error Row: {TypeOfAssignment.NodePosition.Row} Column:{ TypeOfAssignment.NodePosition.Column} La propiedad tiene {baseTypeOfVariable.LenghtOfProperties} propiedas de arreglo de mas");
                if (baseTypeOfVariable != baseTypeAssigment)
                    throw new SemanticException($"Error Row:{ValueOfAssigment.NodePosition.Row} Column:{ValueOfAssigment.NodePosition.Column} la asignacion es incopatible ");

                return baseTypeAssigment;
            }
            var type = Context.StackOfContext.Stack.Peek().GetType(Value);
            type.LenghtOfProperties = (Context.StackOfContext.Stack.Peek().InformatioNVariable.ContainsKey(Value))?  (Context.StackOfContext.Stack.Peek().InformatioNVariable[Value].Lenght) : type.LenghtOfProperties;

            
            foreach (var accesorNode in Accesors)
            {
             type=   accesorNode.ValidateSemantic(type);
               
            }

            if (type.LenghtOfProperties > 0)
                throw new SemanticException("Jimmy restar es lo mejor loco");
            return type;
        }

        public override string GenerateCode()
        {
            if (Accesors.Count == 0)
                return $"{Value}";
            string accesors = Accesors.Aggregate("", (current, accesorNode) => current + accesorNode.GenerateCode());

            return Value + accesors;

        }

        public override Value Interpretation()
        {
            throw new NotImplementedException();
        }
    }
}
