﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class MultNode : BinaryOperator
    {
        public MultNode()
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
           return GetCode("*");
       }
    }
}
