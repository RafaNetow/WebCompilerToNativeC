﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class LessThanOperatorNode: BinaryOperator
    {
        public LessThanOperatorNode()
        {
            Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                            Context.StackOfContext.GetType("int")),
                        Context.StackOfContext.GetType("bool")
                    },   {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("bool"),
                            Context.StackOfContext.GetType("float")),
                        Context.StackOfContext.GetType("bool")
                    },  {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("bool"),
                            Context.StackOfContext.GetType("int")),
                        Context.StackOfContext.GetType("bool")
                    }, {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("string"),
                            Context.StackOfContext.GetType("string")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("char"),
                            Context.StackOfContext.GetType("string")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("char"),
                            Context.StackOfContext.GetType("char")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("bool"),
                            Context.StackOfContext.GetType("bool")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                            Context.StackOfContext.GetType("bool")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("float"),
                            Context.StackOfContext.GetType("int")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                            Context.StackOfContext.GetType("float")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("float"),
                            Context.StackOfContext.GetType("float")),
                        Context.StackOfContext.GetType("bool")
                    }




           };
        }

        public override string GenerateCode()
       {
           return GetCode("<");
       }

        public override Value Interpretation()
        {
            dynamic left = LeftOperand.Interpretation();
            dynamic right = RightOperand.Interpretation();


            dynamic response = left.Value < right.Value;

            dynamic typeOfReturn = GetTypeValue(left, right, response);

            return typeOfReturn;
        }

        public override object GetTypeValue(object right, object left, dynamic value)
        {
            if (right is StringValue && left is StringValue)
                return new BoolValue() { Value = value };

            if (right is IntValue && left is IntValue)
                return new BoolValue() { Value = value };

            if (right is IntValue && left is FloatValue)
                return new BoolValue() { Value = value };

            if (right is FloatValue && left is IntValue)
                return new BoolValue() { Value = value };

            if( right is FloatValue && left is FloatValue)
                return new BoolValue {Value = value};
            if (right is CharValue && left is StringValue)
                return new BoolValue() { Value = value };

            if (right is CharValue && left is CharValue)
                return new BoolValue() { Value = value };

            if (right is StringValue && left is CharValue)
                return new BoolValue() { Value = value };

            if (right is BoolValue && left is BoolValue)
                return new BoolValue() { Value = value };

            if (right is BoolValue && left is IntValue)
                return new BoolValue() { Value = value };

            if (right is IntValue && left is BoolValue)
                return new BoolValue() { Value = value };
            //     return new IntValue();



            return null;
        }
    }
}
