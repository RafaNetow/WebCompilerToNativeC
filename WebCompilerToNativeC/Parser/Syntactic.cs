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
       public HandlerToSyntactic  Hanlder = new HandlerToSyntactic();

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
           else if (CompareTokenType(TokenTypes.OpenBracket))
           {
               IsArrayDeclaration();
           }
           else if (CompareTokenType(TokenTypes.Eos))
           {
               ConsumeNextToken();
           }
            else if (CompareTokenType(TokenTypes.LParenthesis))
            {
                IsFunctionDeclration();
                ConsumeNextToken();
                ParameterList();



            }
            else
            {
                Hanlder.DefaultError(_currentToken);
            }

        }

        private void IsFunctionDeclration()
        {
            ConsumeNextToken();
            ParameterList();


        }

        private void ParameterList()
        {
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
                ChooseIdType();
                if (CompareTokenType(TokenTypes.Comma))
                {
                    OptionalListOfParams();
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
            

        }

        private void OptionalListOfParams()
        {
           ConsumeNextToken();
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
                ChooseIdType();
                if (CompareTokenType(TokenTypes.Comma))
                {
                    OptionalListOfParams();
                }
                else
                {

                }
            }
            Hanlder.DefaultError(_currentToken);

        }

        private void ChooseIdType()
        {
            Result result;
            //Revisar si esta parte del codigo va a funcionar
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.AndBinary))
            {   ConsumeNextToken();
               result=  Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                if (result.Succes)
                {
                    ConsumeNextToken();
                }
                throw result.Excpetion;


            }
            else if (CompareTokenType(TokenTypes.Mul))
            {
                IsPointer();
                
                result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                if (result.Succes)
                {
                    ConsumeNextToken();
                }
                else
                {
                    throw result.Excpetion;
                }
            }else if (CompareTokenType(TokenTypes.Id))
            {
                ConsumeNextToken();
            }
            else
            {
                Hanlder.DefaultError(_currentToken);
            }


        }

        private void IsArrayDeclaration()
        { ConsumeNextToken();
            SizeForArray();
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.OpenBracket))
                BidArray();
            if (CompareTokenType(TokenTypes.Asiggnation))
                OptionalInitOfArray();
            if (CompareTokenType(TokenTypes.Eos))
                ConsumeNextToken();
            else
            {
                
            }
        }

        private void OptionalInitOfArray()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Lbrace))
            {
                ListOfExpressions();
                if (CompareTokenType(TokenTypes.Rbrace))
                    ConsumeNextToken();
            }
            else
            {
                throw new SyntacticException("Expected a  ;", _currentToken.Row, _currentToken.Column);
            }
        }

        private void BidArray()
        {
            SizeBidArray();
            if (CompareTokenType(TokenTypes.CloseBracket))
                ConsumeNextToken();
            else
                throw new SyntacticException("Expected a CloseBracket", _currentToken.Row, _currentToken.Column);
        }

        private void SizeBidArray()
        {
            if (CompareTokenType(TokenTypes.Id) ||
                CompareTokenType(TokenTypes.NumericalLiteral) ||
                CompareTokenType(TokenTypes.HexadecimalLiteral) ||
                CompareTokenType(TokenTypes.OctalLietral))
                ConsumeNextToken();
            else
               throw new SyntacticException("Do not can initializer an array with the type of identifeir", _currentToken.Row, _currentToken.Column);
        
    }

        private void SizeForArray()
        {
            if(CompareTokenType(TokenTypes.Id) || 
                CompareTokenType(TokenTypes.NumericalLiteral) ||
                CompareTokenType(TokenTypes.HexadecimalLiteral) ||
                CompareTokenType(TokenTypes.OctalLietral)) 
                ConsumeNextToken();
            else if (CompareTokenType(TokenTypes.RParenthesis)) { }
          
            else   
                throw  new SyntacticException("Un Expected Token, Array do not support this type",_currentToken.Row, _currentToken.Column);
               

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


        //Compare if exist some retalacion operation that should return some bool or some similar
        private void RelationalExpressionPrime()
        {

            if (RWords.RelationalOperators.ContainsKey(_currentToken.Type))
            {
                RelationalOperators();
                ExpressionAdicion();
                RelationalExpressionPrime();
            }

        }

        private void RelationalOperators()
        {
           ConsumeNextToken();
        }

        public void RelationalExpression()
        {
            ExpressionAdicion();
            RelationalExpressionPrime();

        }

        private void ExpressionAdicion()
        {

            ExpressionMul();
            ExpressionAdicionPrime();
        }

        public void ExpressionAdicionPrime()
        {
            if (CompareTokenType(TokenTypes.Sub) || CompareTokenType(TokenTypes.Sum))
            {
              
                AdditiveOperators();
                ExpressionMul();
                ExpressionAdicion();
            }

        }

        private void AdditiveOperators()
        {
            ConsumeNextToken();

        }

        private void ExpressionMul()
        {
            
                ExpressionUnary();
                ExpressionMulPrime();
           // }
          //  Factor();

        }

        private void ExpressionMulPrime()
        {
            if (CompareTokenType(TokenTypes.Mul) || CompareTokenType(TokenTypes.Div) ||
                CompareTokenType(TokenTypes.Modulus))
            {
                ConsumeNextToken();
                ExpressionUnary();
                ExpressionMulPrime();
            }
            else
            {
                
            }
            

        }

        public void Factor()
        {
        if (CompareTokenType(TokenTypes.Id))
                FactorFunArray();
            //Verify if expression could begin with LPARENT of if haved to consum that token after
            else if (CompareTokenType(TokenTypes.RParenthesis))             
                Expression();

        //Verificar bien o de literales Booleanas
         else if (CompareTokenType(TokenTypes.NumericalLiteral) || CompareTokenType(TokenTypes.StringLiteral) ||
                  CompareTokenType(TokenTypes.DateLiteral) || CompareTokenType(TokenTypes.NumericalLiteral) ||
                  CompareTokenType(TokenTypes.CharLiteral) || CompareTokenType(TokenTypes.BooleanLiteral))
         {
                ConsumeNextToken();
         }
         else
         {
             throw new SyntacticException("Expected a factor", _currentToken.Row, _currentToken.Column);
         }
        }
        /* In this production could be three case
        *CallFunction
        *Index,Pointer or Some Point to Acces  */
        public void FactorFunArray()
        {

            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.LParenthesis))
                CallFunction();
            

    
        }

        public void CallFunction()
        {
            ConsumeNextToken();
            ListOfExpressions();
            CompareTokenType(TokenTypes.RParenthesis);
        }


        public void ExpressionUnary()
        {
            if (CompareTokenType(TokenTypes.Increment) || CompareTokenType(TokenTypes.Decrement) ||
                CompareTokenType(TokenTypes.AndBinary) || CompareTokenType(TokenTypes.ComplementBinary) ||
                CompareTokenType(TokenTypes.OrBinary) || CompareTokenType(TokenTypes.XorBinary) ||
                CompareTokenType(TokenTypes.Sub))
            {
                ConsumeNextToken();
            }
            Factor();
        }

        public void ListOfExpressions()
        {
            Expression();
            if(CompareTokenType(TokenTypes.Comma))
            OptionalExpression();

        }

        public void OptionalExpression()
        {   ConsumeNextToken();
            ListOfExpressions();

        }

        public void IsPointer()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Mul))
                IsPointer();



        }
    }

   
}
