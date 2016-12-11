using WebCompilerToNativeC.Semantic.BaseTypes;

namespace WebCompilerToNativeC.Semantic.BaseClass
{
   public abstract class BaseType
   {
       public abstract bool IsAssignable(BaseType otherType);
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

            //if (right is DateType)
            //{
            //    return left is DateType;
            //}

            return false;
        }


    }
}
