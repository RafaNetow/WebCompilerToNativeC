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
        public Dictionary<string, TokenTypes> SpecialOperators;
        public List<char> SpecialSymbols;


        public ReserverdWords()
        {
            Operators = new Dictionary<string, TokenTypes>();
            ReserverWords = new Dictionary<string, TokenTypes>();
            SpecialOperators = new Dictionary<string, TokenTypes>();
            
            InitOperatorsDictionary();
            InitReservdWords();
            InitSpecialOperator();
            InitSpecialSymbols();

        }

       private void InitSpecialSymbols()
       {
            SpecialSymbols.Add('>');
            SpecialSymbols.Add('>');
            SpecialSymbols.Add('<');
            SpecialSymbols.Add('=');
            SpecialSymbols.Add('!');
            SpecialSymbols.Add('&');
            SpecialSymbols.Add('|');
            SpecialSymbols.Add('-');
            SpecialSymbols.Add('+');
            SpecialSymbols.Add('*');
            SpecialSymbols.Add('/');
            SpecialSymbols.Add('%');
            SpecialSymbols.Add('&');
            SpecialSymbols.Add('^');
        }

       private void InitReservdWords()
       {
           ReserverWords.Add("int", TokenTypes.RwInt);
           ReserverWords.Add("float", TokenTypes.RwFloat);
           ReserverWords.Add("char", TokenTypes.RwChar);
           ReserverWords.Add("bool", TokenTypes.RwBool);
           ReserverWords.Add("string", TokenTypes.RwString);
           ReserverWords.Add("date", TokenTypes.RwDate);
           ReserverWords.Add("const", TokenTypes.RwConst);
           ReserverWords.Add("enum", TokenTypes.RwEnum);
           ReserverWords.Add("if", TokenTypes.RwIf);
           ReserverWords.Add("while", TokenTypes.RwWhile);
           ReserverWords.Add("do", TokenTypes.RwDo);
           ReserverWords.Add("for", TokenTypes.RwFor);
           ReserverWords.Add("switch", TokenTypes.RwSwitch);
           ReserverWords.Add("break", TokenTypes.RwBreak);
           ReserverWords.Add("continue", TokenTypes.RwContinue);

        }

        private void InitSpecialOperator()
       {
            SpecialOperators.Add("++", TokenTypes.Increment);
            SpecialOperators.Add("--", TokenTypes.Decrement);
            SpecialOperators.Add("==", TokenTypes.IfEqual);
            SpecialOperators.Add("!=", TokenTypes.UnEqual);
            SpecialOperators.Add(">=", TokenTypes.GreaterThanOrEqual);
            SpecialOperators.Add("<=", TokenTypes.LessThanOrEqual);
            SpecialOperators.Add("&&", TokenTypes.And);
            SpecialOperators.Add("||", TokenTypes.Or);
            SpecialOperators.Add("<<", TokenTypes.LeftShift);
            SpecialOperators.Add(">>", TokenTypes.LeftShift);
            SpecialOperators.Add("+=", TokenTypes.AddAndAssignment);
            SpecialOperators.Add("+=", TokenTypes.SubAndAssignment);
            SpecialOperators.Add("*=", TokenTypes.MulAndAssignment);
            SpecialOperators.Add("/=", TokenTypes.DivAndAssignment);
            SpecialOperators.Add("%=", TokenTypes.ModulAndAssignment);
            SpecialOperators.Add("&=", TokenTypes.BitwiseAndAndAssignment);
            SpecialOperators.Add("^=", TokenTypes.BitwiseExclusiveOrAndAssignment);
            SpecialOperators.Add("|=", TokenTypes.BitwiseInclusiveOrAndAssignment);

        }

       private void InitOperatorsDictionary()
       {
            Operators.Add("/", TokenTypes.Div);
            Operators.Add("+",TokenTypes.Sum);
            Operators.Add("*",TokenTypes.Mul);
            Operators.Add("-",TokenTypes.Sub);
            Operators.Add(">",TokenTypes.GreaterThan);
            Operators.Add("<",TokenTypes.LessThan);
            Operators.Add("(",TokenTypes.LParenthesis);
            Operators.Add(")",TokenTypes.RParenthesis);
           Operators.Add("[", TokenTypes.OpenBracket);
            Operators.Add("]",TokenTypes.CloseBracket);
            Operators.Add("%",TokenTypes.Modulus);
            Operators.Add("=", TokenTypes.Asiggnation);        
            Operators.Add("!",TokenTypes.Not);
            Operators.Add("&",TokenTypes.AndBinary);
            Operators.Add("|",TokenTypes.OrBinary);
            Operators.Add("^",TokenTypes.XorBinary);
            Operators.Add("~", TokenTypes.ComplementBinary);
            Operators.Add("*", TokenTypes.Pointer);
           






        }
    }
}
