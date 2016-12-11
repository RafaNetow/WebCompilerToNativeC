using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
  public   class AssignationBinary : BinaryOperator
    {
      public override BaseType ValidateSemantic()
      {
          throw new NotImplementedException();
      }

      public override string GenerateCode()
      {
          throw new NotImplementedException();
      }
    }
}
