using System;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType
{
    public class DateNode : BaseClass.DataType
    {
        public string DateValue;


        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
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
