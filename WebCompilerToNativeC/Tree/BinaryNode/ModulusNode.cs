using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class ModulusNode : BinaryOperator
    {

       public ModulusNode()
       {
           Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                   new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                       TypesTable.Instance.GetType("int")),
                   TypesTable.Instance.GetType("int")
               }
           };
       }

       public override string GenerateCode()
       {
         return  GetCode("%");
       }
    }
}
