using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Lexer
{
   public class ReserverdWords
    {
        public Dictionary<string, TokenTypes> Operators;
        public Dictionary<string, TokenTypes> ReserverWords;
        public Dictionary<string, TokenTypes> SpecialReserverdWords;
        public List<char> SpecialSymbols;


        public ReserverdWords()
        {
            Operators = new Dictionary<string, TokenTypes>();
            ReserverWords = new Dictionary<string, TokenTypes>();
            SpecialSymbols = new List<char>();
            SpecialReserverdWords = new Dictionary<string, TokenTypes>();
            InitOperatorsDictionary();
            InitReservdWords();
            InitSpecialOperator();

        }

       private void InitOperatorsDictionary()
       {
            Operators.Add("+",TokenTypes.Sum);
            Operators.Add("*",TokenTypes.Mul);
            Operators.Add("-",TokenTypes.Sub);
            Operators.Add("(",TokenTypes.LParent);


           

       }
    }
}
