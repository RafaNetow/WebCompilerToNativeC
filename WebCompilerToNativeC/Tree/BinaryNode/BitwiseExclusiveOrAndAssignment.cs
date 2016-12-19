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
   public class BitwiseExclusiveOrAndAssignment : BinaryOperator
    {
       public BitwiseExclusiveOrAndAssignment()
       {
           Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("char"),
                       Context.StackOfContext.GetType("int")),
                   Context.StackOfContext.GetType("int")
               }, {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                       Context.StackOfContext.GetType("char")),
                   Context.StackOfContext.GetType("int")
               }, {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                       Context.StackOfContext.GetType("int")),
                   Context.StackOfContext.GetType("int")
               },
                {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("char"),
                       Context.StackOfContext.GetType("char")),
                   Context.StackOfContext.GetType("int")
               }
           };
       }

       public override string GenerateCode()
       {
           return GetCode("^=");
       }

        public override Value Interpretation()
        {
            dynamic left = LeftOperand.Interpretation();
            dynamic right = RightOperand.Interpretation();


            dynamic response = left.Value ^= right.Value;

            dynamic typeOfReturn = GetTypeValue(left, right, response);

            return typeOfReturn;
        }

        public override object GetTypeValue(object right, object left, dynamic value)
        {


            if (right is CharValue && left is IntValue)
                return new IntValue() { Value = value };

            if (right is IntValue && left is CharValue)
                return new IntValue() { Value = value };

            if (right is IntValue && left is IntValue)
                return new IntValue() { Value = value };

            if (right is CharValue && left is CharValue)
                return new IntValue() { Value = value };


            //     return new IntValue();



            return null;
        }
    }

    class BitwiseExclusiveOrAndAssignmentImpl : BitwiseExclusiveOrAndAssignment
    {
    }
}
