using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;

namespace WebCompilerToNativeC.Tree
{
    public class NotEqualt : BinaryOperator
    {
        public NotEqualt()
        {
            Validation = new Dictionary<Tuple<BaseType, BaseType>, BaseType>
           {

               {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("int"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("bool")
                    },   {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("bool"),
                            TypesTable.Instance.GetType("float")),
                        TypesTable.Instance.GetType("bool")
                    },  {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("bool"),
                            TypesTable.Instance.GetType("int")),
                        TypesTable.Instance.GetType("bool")
                    }, {
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("string"),
                            TypesTable.Instance.GetType("string")),
                        TypesTable.Instance.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("char"),
                            TypesTable.Instance.GetType("string")),
                        TypesTable.Instance.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("char"),
                            TypesTable.Instance.GetType("char")),
                        TypesTable.Instance.GetType("bool")
                    },{
                        new Tuple<BaseType, BaseType>(TypesTable.Instance.GetType("bool"),
                            TypesTable.Instance.GetType("bool")),
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
            return GetCode("!=");
        }
    }
}
