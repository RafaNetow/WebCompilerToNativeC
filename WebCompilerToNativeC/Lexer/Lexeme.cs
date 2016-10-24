using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Lexer
{
    public class Lexeme
    {
        public string Value { get; set; }
        public  int Row { get; set; }
        public  int Columns { get; set; }
    }
}
