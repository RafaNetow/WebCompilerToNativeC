using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseClass.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes;

namespace WebCompilerToNativeC.interpretation.Interpretation
{
   public static  class TypesValidation
   {

       public static Dictionary<string, Value> TableValue = new Dictionary<string, Value>()
       {
           { "string",
           new StringValue()},
           {  
           "int", new IntValue()
           
           },
           {
             "bool", new BoolValue()
           },
           { "date", new DateValue() },
           {
               "char", new CharValue()
           },
           {
               "float", new FloatValue()
           }




       };


        public static Value GetTypeValue(Value type)
        {
            if (type is StringValue)
                return new StringValue();
            if (type is IntValue)
                return new IntValue();
            if (type is BoolValue)
                return new BoolValue();
            if (type is DateValue)
                return new DateValue();
            if (type is CharValue)
                return new CharValue();
            if (type is FloatValue)
                return new FloatValue();

            return null;
        }

       public static object GetTypeValue(object right,object left, dynamic value)
       {
           if(right is StringValue && left is StringValue)
                return new StringValue() {Value = value};
           //if(type is IntValue)
           //     return new IntValue();
           //if(type is BoolValue)
           //     return new BoolValue();
           //if(type is DateValue)
           //     return new DateValue();
           //if(type is CharType)
           //     return new CharValue();
           //if(type is FloatValue)
           //     return new FloatValue();



           return null;
       }

      

        public static bool EquivalenceOfType(BaseType typeToCompare, BaseType typeOfReturn)
        {
            if (typeToCompare is IntType || typeToCompare is FloatType || typeToCompare is ConstType)
            {
                return typeOfReturn is IntType || typeOfReturn is FloatType || typeOfReturn is BooleanType || typeOfReturn is CharType;
            }
            if (typeToCompare is BooleanType)
                return typeOfReturn is IntType || typeOfReturn is BooleanType;

            if (typeToCompare is CharType || typeToCompare is StringType)
                return typeOfReturn is IntType || typeOfReturn is BooleanType || typeOfReturn is CharType || typeOfReturn is StringType;

            if (typeToCompare is DateType)
                return typeOfReturn is DateType;

            return false;
        }
    }
}
