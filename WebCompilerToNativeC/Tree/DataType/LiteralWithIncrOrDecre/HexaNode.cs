using System;
using WebCompilerToNativeC.interpretation.BaseClass;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree.DataType.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre
{
   public class HexaNode : LiteralWithOptionalIncrementOrDecrement
   {
       public string HexValue;

        public override BaseType ValidateSemantic()
       {
            return Context.StackOfContext.GetType("int"); ;
       }

       public override string GenerateCode()
       {
           return $"{Value}";
       }

       public override Value Interpretation()
       {
           throw new NotImplementedException();
       }

       public override void SetValue(string value)
       {
           HexValue = value;
       }
   }
}
