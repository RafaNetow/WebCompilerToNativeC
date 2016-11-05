using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Lexer;

namespace WebCompilerToNativeC.Parser
{
    public class Syntactic
    {
        private Lexer.Lexer _lexer;
        private Token _currentToken;
        public ReserverdWords RWords = new ReserverdWords();

        public Syntactic( Lexer.Lexer lexer)
        {
            _lexer = lexer;
            ConsumeNextToken();
        }
        /***Helpers Functionts *****/
        public void ConsumeNextToken()
        {
            _currentToken = _lexer.GetNextToken();
        }
        public bool CompareTokenType(TokenTypes type)
        {
            return _currentToken.Type == type;
        }
        /***************************/

        public void Parser()
        {
            Sentence();
        }

        public void Sentence()
        {
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {

                Declaretion();
                GeneralDeclration();
            }
        }

        public void Declaretion()
        {
            GeneralDeclration();
        }

        //General declartion is DataTye Pointer Identifeir
        public void GeneralDeclration()
        {
            ConsumeNextToken();

            if(CompareTokenType(TokenTypes.Mul))
            IsPointer();

            if (CompareTokenType(TokenTypes.Id))
            {
                TypeOfDeclaration();
            }
            else
            {
                throw new SyntacticException("Expected some Id", _currentToken.Row, _currentToken.Column);
            }
            
        }

        public void TypeOfDeclaration()
        {
            if (CompareTokenType(TokenTypes.Asiggnation))
                ValueForId();

        }

        private void ValueForId()
        {
            ConsumeNextToken();
            Expression();

        }

        private void Expression()
        {
            RelationalExpression();

        }

        public void RelationalExpression()
        {
            ExpressionAdicion();

        }

        private void ExpressionAdicion()
        {

            ExpressionMul();
        }

        private void ExpressionMul()
        {
            if (CompareTokenType(TokenTypes.Increment) || CompareTokenType(TokenTypes.Decrement) ||
               CompareTokenType(TokenTypes.AndBinary) || CompareTokenType(TokenTypes.ComplementBinary) ||
               CompareTokenType(TokenTypes.OrBinary) || CompareTokenType(TokenTypes.XorBinary) ||
               CompareTokenType(TokenTypes.Sub))
            {
                ExpressionUnary();
            }
            Factor();

        }

        private void Factor()
        {///asdas
            

        }

        public void ExpressionUnary()
        {
            

        }

        public void IsPointer()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Mul))
                IsPointer();


        }
    }

  
}
