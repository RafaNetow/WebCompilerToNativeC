using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Lexer
{
    public class LexerException : Exception
    {
        private Symbol currentSymbol;

        public LexerException()
        {
        }

        public LexerException(string message) : base(message)
        {
        }

        public LexerException(Symbol currentSymbol)
        {
            this.currentSymbol = currentSymbol;
        }

        public LexerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LexerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
