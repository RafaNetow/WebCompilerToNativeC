using System;
using System.Collections.Generic;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
namespace WebCompilerToNativeC.Tree
{
    public class AddAndAssignmentNode : BinaryOperator
    {
        public AddAndAssignmentNode()
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
                    }, {
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("string"),
                            Context.StackOfContext.GetType("string")),
                        Context.StackOfContext.GetType("string")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("char"),
                            Context.StackOfContext.GetType("string")),
                        Context.StackOfContext.GetType("string")
                    },{
                        new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("string"),
                            Context.StackOfContext.GetType("char")),
                        Context.StackOfContext.GetType("string")
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
            return GetCode("+=");
        }

        public override Value Interpretation()
        {
            throw new NotImplementedException();
        }
    }
}
