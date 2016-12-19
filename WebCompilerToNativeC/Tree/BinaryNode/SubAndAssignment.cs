using System;
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
   public class SubAndAssignment : BinaryOperator
    {
        public SubAndAssignment()
        {
            Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                            Context.StackOfContext.GetType("int")),
                        Context.StackOfContext.GetType("int")
                    },   {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                            Context.StackOfContext.GetType("float")),
                        Context.StackOfContext.GetType("float")
                    },  {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("float"),
                            Context.StackOfContext.GetType("int")),
                        Context.StackOfContext.GetType("float")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("bool"),
                            Context.StackOfContext.GetType("bool")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("bool"),
                            Context.StackOfContext.GetType("int")),
                        Context.StackOfContext.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                            Context.StackOfContext.GetType("bool")),
                        Context.StackOfContext.GetType("bool")
                    }




           };
        }

        public override string GenerateCode()
       {
           return GetCode("-="); 
       }

        public override Value Interpretation()
        {
            dynamic left = LeftOperand.Interpretation();
            dynamic right = RightOperand.Interpretation();


            dynamic response = left.Value -=
                right.Value;

            dynamic typeOfReturn = GetTypeValue(left, right, response);

            return typeOfReturn;
        }

        public override object GetTypeValue(object right, object left, dynamic value)
        {


            if (right is IntValue && left is IntValue)
                return new IntValue() { Value = value };

            if (right is IntValue && left is FloatValue)
                return new FloatValue() { Value = value };

            if (right is FloatValue && left is IntValue)
                return new FloatValue() { Value = value };

            if (right is FloatValue && left is FloatValue)
                return new FloatValue() { Value = value };

            if (right is CharValue && left is CharValue)
                return new StringValue() { Value = value };


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
