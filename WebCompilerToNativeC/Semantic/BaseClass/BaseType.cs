using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic.BaseClass.BaseTypes;
using WebCompilerToNativeC.Semantic.BaseTypes;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
   public abstract class BaseType
   {
       public abstract bool IsAssignable(BaseType otherType);
       public abstract Value GetDefaultValue();
       public int LenghtOfProperties;
        public  bool BaseTypeEquivalent(BaseType typeToCompare, BaseType typeToReturn)
        {
            if (typeToCompare is IntType || typeToCompare is FloatType)
            {
                return typeToReturn is IntType || typeToReturn is FloatType || typeToReturn is BooleanType || typeToReturn is CharType;
            }

            if (typeToCompare is BooleanType)
            {
                return typeToReturn is IntType || typeToReturn is BooleanType;
            }

            if (typeToCompare is CharType || typeToCompare is StringType)
            {
                return typeToReturn is IntType || typeToReturn is BooleanType || typeToReturn is CharType || typeToReturn is StringType;
            }

            if (typeToCompare is DateType)
           {
               return typeToReturn is DateType;
           }

            return false;
        }


    }
}
