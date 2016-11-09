using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Parser
{
    public class SyntacticException: Exception
    {
        public SyntacticException(string message, int row, int column)  : base( message + "Column  : " + column.ToString() + " Row  : " + row.ToString())
        {
            
        }
    }
}
