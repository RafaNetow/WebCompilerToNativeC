using System;
using System.Collections.Generic;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.interpretation.Interpretation;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class AddNode : BinaryOperator
    {

       public AddNode()
       {
           Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                       Context.StackOfContext.GetType("int")),
                   Context.StackOfContext.GetType("int")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                       Context.StackOfContext.GetType("float")),
                   Context.StackOfContext.GetType("float")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("float"),
                       Context.StackOfContext.GetType("int")),
                   Context.StackOfContext.GetType("float")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("string"),
                       Context.StackOfContext.GetType("string")),
                   Context.StackOfContext.GetType("string")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("char"),
                       Context.StackOfContext.GetType("string")),
                   Context.StackOfContext.GetType("string")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("char"),
                       Context.StackOfContext.GetType("char")),
                   Context.StackOfContext.GetType("string")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("string"),
                       Context.StackOfContext.GetType("char")),
                   Context.StackOfContext.GetType("string")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("bool"),
                       Context.StackOfContext.GetType("bool")),
                   Context.StackOfContext.GetType("bool")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("bool"),
                       Context.StackOfContext.GetType("int")),
                   Context.StackOfContext.GetType("bool")
               },
               {
                   new Tuple<BaseType, BaseType>(Context.StackOfContext.GetType("int"),
                       Context.StackOfContext.GetType("bool")),
                   Context.StackOfContext.GetType("bool")
               }




           };
            ValidationValue = new Dictionary<Tuple<Value, Value>, Value>
              {

                  {
                           new Tuple<Value, Value>(TypesValidation.TableValue["int"],
                               TypesValidation.TableValue["int"]),
                           TypesValidation.TableValue["int"]
                       },   {
                           new Tuple<Value, Value>(TypesValidation.TableValue["int"],
                              TypesValidation.TableValue["float"]),
                               TypesValidation.TableValue["float"]
                       },
                             {
                           new Tuple<Value, Value>(TypesValidation.TableValue["float"],
                              TypesValidation.TableValue["int"]),
                               TypesValidation.TableValue["float"]
                       },   {
                           new Tuple<Value, Value>(TypesValidation.TableValue["string"],
                              TypesValidation.TableValue["string"]),
                               TypesValidation.TableValue["string"]
                       },
                            {
                           new Tuple<Value, Value>(TypesValidation.TableValue["char"],
                              TypesValidation.TableValue["string"]),
                               TypesValidation.TableValue["string"]
                       },{
                           new Tuple<Value, Value>(TypesValidation.TableValue["char"],
                              TypesValidation.TableValue["char"]),
                               TypesValidation.TableValue["string"]
                       }
                     ,{
                           new Tuple<Value, Value>(TypesValidation.TableValue["string"],
                              TypesValidation.TableValue["char"]),
                               TypesValidation.TableValue["string"]
                       }

                           ,{
                           new Tuple<Value, Value>(TypesValidation.TableValue["bool"],
                              TypesValidation.TableValue["bool"]),
                               TypesValidation.TableValue["bool"]
                                    }
                        ,{
                           new Tuple<Value, Value>(TypesValidation.TableValue["bool"],
                              TypesValidation.TableValue["int"]),
                               TypesValidation.TableValue["bool"]
                                    },
                            {
                           new Tuple<Value, Value>(TypesValidation.TableValue["int"],
                              TypesValidation.TableValue["bool"]),
                               TypesValidation.TableValue["bool"]
                                    }
                       




              };
        }
    

       public override string GenerateCode()
       {
           return GetCode("+");
       }

       public override Value Interpretation()
       {
            dynamic left = LeftOperand.Interpretation();
            dynamic right = RightOperand.Interpretation();


            dynamic response = left.Value + right.Value;

            dynamic typeOfReturn = GetTypeValue(left,right, response);

            return typeOfReturn;
        }

        public override object GetTypeValue(object right, object left, dynamic value)
        {
            if (right is StringValue && left is StringValue)
                return new StringValue() { Value = value };

              if(right is IntValue && left  is IntValue)
                return new IntValue() {Value = value};

            if (right is IntValue && left is FloatValue)
                return new FloatValue() { Value = value };

            if (right is FloatValue && left is IntValue)
                return new FloatValue() { Value = value };

            if (right is CharValue && left is StringValue)
                return new StringValue() { Value = value };

            if (right is CharValue && left is CharValue)
                return new StringValue() { Value = value };

            if (right is StringValue && left is CharValue)
                return new StringValue() { Value = value };

            if (right is BoolValue && left is BoolValue)
                return new BoolValue() { Value = value };

            if (right is BoolValue && left is IntValue)
                return new BoolValue() { Value = value };

            if (right is IntValue && left is BoolValue)
                return new BoolValue() { Value = value };
            //     return new IntValue();
            if (right is FloatValue && left is FloatValue)
                return new FloatValue() { Value = value };


            return null;
        }

        
    }
}
