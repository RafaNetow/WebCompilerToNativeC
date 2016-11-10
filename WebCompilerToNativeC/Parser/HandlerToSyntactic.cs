using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Lexer;

namespace WebCompilerToNativeC.Parser
{
   public class HandlerToSyntactic
    {


       public Result CheckToken(TokenTypes expectToken, Token currentToken)
       {

           if (expectToken == currentToken.Type)
               return new Result() {Succes = true, Excpetion = null};

           return new Result {Succes = false,Excpetion = new SyntacticException($"Expected a {expectToken.ToString()} Token ",currentToken.Row,currentToken.Column)};


       }

        //When dnt found any production that could be allowed
       public Exception DefaultError(Token currentToken)
       {
            return new  SyntacticException("UnExpected Token"+currentToken.Type.ToString() +" ",currentToken.Row,currentToken.Column);
    }
    }

    public class Result
    {
        public bool Succes { get; set; }
        public SyntacticException Excpetion { get; set; }
    }
}
