using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.interpretation.DataTypes;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Semantic.BaseTypes;

namespace WebCompilerToNativeC.Tree.DataType.DateNode
{
    public class DateNode : BaseClass.DataType
    {
        public string DateValue;


        public override BaseType ValidateSemantic()
        {
              return Context.StackOfContext.GetType("date");
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override Value Interpretation()
        {

            int day = Convert.ToInt32(Value.Substring(1, 2));
    
            int year = Convert.ToInt32(Value.Substring(7, 4));
            int month = Convert.ToInt32(Value.Substring(4, 2));
          
            return new DateValue { Value = new DateTime(year, month, day) };
        }

        public override void SetValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
