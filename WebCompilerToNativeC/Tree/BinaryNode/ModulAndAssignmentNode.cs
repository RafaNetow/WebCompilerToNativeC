﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class ModulAndAssignmentNode : BinaryOperator
    {
        public ModulAndAssignmentNode()
        {
            Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                       Context.StackOfContext.GetType("int")),
                   Context.StackOfContext.GetType("int")
               }
           };
        }

        public override string GenerateCode()
       {
           return GetCode("%=");
       }

       public override Value Interpretation()
       {
           throw new NotImplementedException();
       }

       public override object GetTypeValue(object right, object left, dynamic value)
       {
           throw new NotImplementedException();
       }
    }
}
