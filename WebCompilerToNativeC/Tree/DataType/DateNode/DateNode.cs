using System;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType
{
    public class DateNode : BaseClass.DataType
    {
        public string DateValue;


        public override BaseType ValidateSemantic()
        {
          return new DateType();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }

        public override void SetValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
