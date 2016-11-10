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
       public Dictionary<TokenTypes, TokenTypes> RelationalOperators;
        public List<char> SpecialSymbols = new List<char>();
        public  List<string>DataTypes = new List<string>();


        public ReserverdWords()
        {
            Operators = new Dictionary<string, TokenTypes>();
            ReserverWords = new Dictionary<string, TokenTypes>();
            SpecialOperators = new Dictionary<string, TokenTypes>();
            RelationalOperators = new Dictionary<TokenTypes, TokenTypes>();
            
            InitOperatorsDictionary();
            InitReservdWords();
            InitSpecialOperator();
            InitSpecialSymbols();
            InitDataTypes();
            InitalRelationalOperators();

        }

       public void InitalRelationalOperators()
        {
            
            
            RelationalOperators.Add(TokenTypes.IfEqual, TokenTypes.IfEqual);
            RelationalOperators.Add(TokenTypes.UnEqual, TokenTypes.UnEqual);
            RelationalOperators.Add(TokenTypes.GreaterThanOrEqual, TokenTypes.GreaterThanOrEqual);
            RelationalOperators.Add(TokenTypes.LessThanOrEqual, TokenTypes.LessThanOrEqual);
            RelationalOperators.Add(TokenTypes.And, TokenTypes.And);
            RelationalOperators.Add(TokenTypes.Or, TokenTypes.Or);
            RelationalOperators.Add(TokenTypes.LeftShift, TokenTypes.LeftShift);
            RelationalOperators.Add(TokenTypes.LessThan, TokenTypes.LessThan);
            RelationalOperators.Add(TokenTypes.GreaterThan, TokenTypes.GreaterThan);
            RelationalOperators.Add(TokenTypes.AddAndAssignment, TokenTypes.AddAndAssignment);
            RelationalOperators.Add(TokenTypes.SubAndAssignment, TokenTypes.SubAndAssignment);
            RelationalOperators.Add(TokenTypes.MulAndAssignment, TokenTypes.MulAndAssignment);
            RelationalOperators.Add(TokenTypes.DivAndAssignment, TokenTypes.DivAndAssignment);
            RelationalOperators.Add(TokenTypes.ModulAndAssignment, TokenTypes.ModulAndAssignment);
            RelationalOperators.Add(TokenTypes.BitwiseAndAndAssignment, TokenTypes.BitwiseAndAndAssignment);
            RelationalOperators.Add(TokenTypes.BitwiseExclusiveOrAndAssignment, TokenTypes.BitwiseExclusiveOrAndAssignment);
            RelationalOperators.Add(TokenTypes.BitwiseInclusiveOrAndAssignment, TokenTypes.BitwiseInclusiveOrAndAssignment);
           



        }

       private void InitDataTypes()
       {
           DataTypes.Add("int");
           DataTypes.Add("float");
           DataTypes.Add("char");
           DataTypes.Add("bool");
           DataTypes.Add("string");
           DataTypes.Add("date");



        }

       private void InitSpecialSymbols()
       {
            
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
           ReserverWords.Add("#include",TokenTypes.RwInclude);
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
           ReserverWords.Add("true",TokenTypes.True);
           ReserverWords.Add("false", TokenTypes.False);
           ReserverWords.Add("else",TokenTypes.RwElse);
           ReserverWords.Add("struct", TokenTypes.RwStruct);
           ReserverWords.Add("case",TokenTypes.RwCase);
           ReserverWords.Add("default",TokenTypes.RwDefault);
           ReserverWords.Add("return", TokenTypes.RwReturn);




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
            SpecialOperators.Add("-=", TokenTypes.SubAndAssignment);
            SpecialOperators.Add("*=", TokenTypes.MulAndAssignment);
            SpecialOperators.Add("/=", TokenTypes.DivAndAssignment);
            SpecialOperators.Add("%=", TokenTypes.ModulAndAssignment);
            SpecialOperators.Add("&=", TokenTypes.BitwiseAndAndAssignment);
            SpecialOperators.Add("^=", TokenTypes.BitwiseExclusiveOrAndAssignment);
            SpecialOperators.Add("|=", TokenTypes.BitwiseInclusiveOrAndAssignment);
            SpecialOperators.Add("->", TokenTypes.reference);

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
            Operators.Add(".", TokenTypes.Point);
            Operators.Add(",", TokenTypes.Comma);
            Operators.Add("{",TokenTypes.Lbrace);
            Operators.Add("}", TokenTypes.Rbrace);
            Operators.Add(" :",TokenTypes.TwoPoints);

            Operators.Add(";", TokenTypes.Eos);
            







        }
    }
}
