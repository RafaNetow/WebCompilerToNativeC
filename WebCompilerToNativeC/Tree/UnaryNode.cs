using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Semantic;

namespace WebCompilerToNativeC.Tree
{
   public abstract class UnaryNode : ExpresionNode
    {
     
       public  ExpresionNode Value { get; set; }

       public string GetCode(string unarySymbol)
       {
           return $"{unarySymbol}+{Value.GenerateCode()} ";
       } 
    }
}
