using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Hanlders;
using WebCompilerToNativeC.Lexer;

namespace WebCompilerToNativeC
{
    class Program
    {
        static void Main(string[] args)
        {
            HanldersFiles handlerFile = new HanldersFiles();
            var sourceCodes = handlerFile.getCode();
            Lexer.Lexer lexer = new Lexer.Lexer(new StringContent(sourceCodes.ToLower()));
            Console.WriteLine(sourceCodes);
            var currentToken = lexer.GetNextToken();
            while (currentToken.Type != TokenTypes.Eof)
            {
                string s =
                    $" Token : {currentToken.Type}, Lexeme : {currentToken.Lexeme},Row : {currentToken.Row}, Column : {currentToken.Column} ";
                Console.WriteLine(s);
               
                currentToken = lexer.GetNextToken();
            }
            Console.ReadKey();


        }
    }
}
