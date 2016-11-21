using System;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Tree.DataType.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre
{
   public class HexaNode : LiteralWithOptionalIncrementOrDecrement
   {
       public string HexValue;

        public override BaseType ValidateSemantic()
       {
           throw new NotImplementedException();
       }

       public override string GenerateCode()
       {
           return $"{Value}";
       }

       public override void SetValue(string value)
       {
           HexValue = value;
       }
   }
}
