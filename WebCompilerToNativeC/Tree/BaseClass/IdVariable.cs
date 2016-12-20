using System;
using System.Collections.Generic;
using System.Linq;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes.Struct;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.UnaryNode;

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



                if (Context.StackOfContext.Stack.Peek().Contains(Value))
                {
                    baseTypeOfVariable.LenghtOfProperties =
                        Context.StackOfContext.Stack.Peek().InformatioNVariable[Value].Lenght;
                }
                else
                {
                    baseTypeOfVariable.LenghtOfProperties = 0;

                }

                if (Accesors != null)
                {
                    foreach (var accesorNode in Accesors)
                    {
                        baseTypeOfVariable = accesorNode.ValidateSemantic(baseTypeOfVariable);

                    }
                }
                if (!(baseTypeOfVariable.LenghtOfProperties == 0))
                    throw new SemanticException($"Error Row: {TypeOfAssignment.NodePosition.Row} Column:{ TypeOfAssignment.NodePosition.Column} La propiedad tiene {baseTypeOfVariable.LenghtOfProperties} propiedas de arreglo de mas");
                if (baseTypeOfVariable != baseTypeAssigment)
                    throw new SemanticException($"Error Row:{ValueOfAssigment.NodePosition.Row} Column:{ValueOfAssigment.NodePosition.Column} la asignacion es incopatible ");

                return baseTypeAssigment;
            }
            var type = Context.StackOfContext.Stack.Peek().GetType(Value);
            type.LenghtOfProperties = (Context.StackOfContext.Stack.Peek().InformatioNVariable.ContainsKey(Value))?  (Context.StackOfContext.Stack.Peek().InformatioNVariable[Value].Lenght) : type.LenghtOfProperties;

            if (Accesors != null)
            {
                foreach (var accesorNode in Accesors)
                {
                    type = accesorNode.ValidateSemantic(type);

                }
            }

            if (Accesors != null)
            {
                if (type.LenghtOfProperties > 0)
                    throw new SemanticException("Jimmy restar es lo mejor loco");
            }
          
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
            int lengthAccesors = 0;
            if (Context.StackOfContext.Stack.Peek().InformatioNVariable.ContainsKey(Value))
                lengthAccesors = Context.StackOfContext.Stack.Peek().InformatioNVariable[Value].Lenght;


            if (TypeOfAssignment == null && ValueOfAssigment == null &&
              lengthAccesors > 0)
            {


               var lenghtOfArray=  Context.StackOfContext.Stack.Peek().InformatioNVariable[Value].Lenght;
                if (lenghtOfArray == 1)
                {
                  
                    dynamic posValue = Accesors.First().Interpretation();
                   
                 return    Context.StackOfContext.Stack.Peek().GetArrayValue(Value, posValue.Value, null);


                }
                if (lenghtOfArray == 2)
                {
                  
                    dynamic posRow = Accesors.First().Interpretation();
                    dynamic posColum = Accesors.Last().Interpretation();
                  
                 return   Context.StackOfContext.Stack.Peek().GetArrayValue(Value, posRow.Value, posColum.Value);
               


                }


            }
            {
                


            }


            if (IncrementOrDecrement is LeftDecrement)
            {
                dynamic valueBefore = Context.StackOfContext.Stack.Peek().GetVariableValue(Value);

                valueBefore.Value = valueBefore.Value - 1;

                Context.StackOfContext.Stack.Peek().SetVariableValue(Value, valueBefore);
            }
             if (IncrementOrDecrement is LeftIncrement)
            {
                dynamic valueBefore =   Context.StackOfContext.Stack.Peek().GetVariableValue(Value).Clone();

                valueBefore.Value = valueBefore.Value + 1;



                Context.StackOfContext.Stack.Peek().SetVariableValue(Value, valueBefore);
            }
             if (IncrementOrDecrement is RightIncrement)
            {
                dynamic valueBefore = Context.StackOfContext.Stack.Peek().GetVariableValue(Value).Clone();

                valueBefore.Value = valueBefore.Value + 1;
                //  valueBefore.Value = valueBefore.Value++;

                Context.StackOfContext.Stack.Peek().SetVariableValue(Value, valueBefore);
            }
             if (IncrementOrDecrement is RightDecrement)
            {
                dynamic valueBefore = Context.StackOfContext.Stack.Peek().GetVariableValue(Value);

                valueBefore.Value = valueBefore.Value - 1;

                Context.StackOfContext.Stack.Peek().SetVariableValue(Value, valueBefore);
            }

            if (ValueOfAssigment != null)
            {
              

                if (lengthAccesors == 0)
                {
                    Context.StackOfContext.Stack.Peek().SetVariableValue(Value, ValueOfAssigment.Interpretation());

                }
                else
                {
                    if (lengthAccesors == 1)
                    {
                        dynamic valueToAssigment = ValueOfAssigment.Interpretation();
                        dynamic posValue = Accesors.First().Interpretation();
                        valueToAssigment.row = posValue.Value;
                        Context.StackOfContext.Stack.Peek().SetArrayVariableValue(Value, valueToAssigment);


                    }
                    if (lengthAccesors == 2)
                    {
                        dynamic valueToAssigment = ValueOfAssigment.Interpretation();
                        dynamic posRow = Accesors.First().Interpretation();
                        dynamic posColum = Accesors.Last().Interpretation();
                        valueToAssigment.row = posRow.Value;
                        valueToAssigment.column = posColum.Value;
                        Context.StackOfContext.Stack.Peek().SetArrayVariableValue(Value, valueToAssigment);


                    }
                        
                   //     Context.StackOfContext.Stack.Peek().SetArrayVariableValue(Value,n);
                }
         
            }

            return Context.StackOfContext.Stack.Peek().GetVariableValue(Value);
        }
    }
}
