using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Tree.BaseClass;

namespace WebCompilerToNativeC.Tree.DataType.BaseClass
{
    public  abstract class DataType : ExpressionNode
    {
        public string Value;
        public List<PointerNode> ListOfPointers;
        public abstract void SetValue(string value);



    }
}
