using System;
using System.Collections.Generic;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class AddNode : BinaryOperator
    {

       public AddNode ()
       {
           Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("int")
                    },   {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                            TypesTable.Instance.GetType("float")),
                        TypesTable.Instance.GetType("float")
                    },  {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("float"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("float")
                    }, {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("string"),
                            TypesTable.Instance.GetType("string")),
                        TypesTable.Instance.GetType("string")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("char"),
                            TypesTable.Instance.GetType("string")),
                        TypesTable.Instance.GetType("string")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("char"),
                            TypesTable.Instance.GetType("char")),
                        TypesTable.Instance.GetType("string")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("string"),
                            TypesTable.Instance.GetType("char")),
                        TypesTable.Instance.GetType("string")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("bool"),
                            TypesTable.Instance.GetType("bool")),
                        TypesTable.Instance.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("bool"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                            TypesTable.Instance.GetType("bool")),
                        TypesTable.Instance.GetType("bool")
                    }




           };
       }

        public override string GenerateCode()
       {
           return GetCode("+");
       }
    }
}
