using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
   public class MulAndAssignment : BinaryOperator
    {
        public MulAndAssignment()
        {
            Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("int")
                    },   {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                            TypesTable.Instance.GetType("float")),
                        TypesTable.Instance.GetType("float")
                    },  {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("float"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("float")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("bool"),
                            TypesTable.Instance.GetType("bool")),
                        TypesTable.Instance.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("bool"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                            TypesTable.Instance.GetType("bool")),
                        TypesTable.Instance.GetType("bool")
                    }




           };
        }
        public override string GenerateCode()
       {
           return GetCode("*=");
       }
    }
}
