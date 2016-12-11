using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<AccesorNode> Accesors = new List<AccesorNode>();
        public ExpressionNode ValueOfAssigment;


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
                    throw new SemanticException("The properties have a more brackets");
                if (baseTypeOfVariable != baseTypeAssigment)
                    throw new SemanticException("el tipo de asignacion no es valida");

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
    }
}
