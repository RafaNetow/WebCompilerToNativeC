using System;
using WebCompilerToNativeC.interpretation.BaseClass;
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
            throw new NotImplementedException();
        }

        public override void SetValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
