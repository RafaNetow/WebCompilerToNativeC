using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebCompilerToNativeC.Lexer;

namespace WebCompilerToNativeC.Parser
{
    public class Syntactic
    {
        private Lexer.Lexer _lexer;
        private Token _currentToken;
        public ReserverdWords RWords = new ReserverdWords();
       public HandlerToSyntactic  Hanlder = new HandlerToSyntactic();
        //To verify when dnt get the token expected
        public Result result;

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

        public Token PeekNextToke()
        {
            var actualToken = _currentToken;
           ConsumeNextToken();
            var nextToken = _currentToken;
            _currentToken = actualToken;
            return nextToken;

        }


        public void Parse()
        {
            ListOfSentences();
        }
        /***************************/

        public void ListOfSentences()
        {
            Sentence();
            if(!CompareTokenType(TokenTypes.Eof) && !CompareTokenType(TokenTypes.Rbrace))
                ListOfSentences();

        }

        public void VerifySintax()
        {
            
        }

        public void Sentence()
        {
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
                Declaretion();
              
            }
            else if (CompareTokenType(TokenTypes.Rbrace))
                return;

            else if(CompareTokenType(TokenTypes.RwElse))
                Else();

            else if (CompareTokenType(TokenTypes.Eos))
                ConsumeNextToken();

            else if (CompareTokenType(TokenTypes.RwIf))
                If();

            else if (CompareTokenType(TokenTypes.RwWhile))
                While();

            else if (CompareTokenType(TokenTypes.RwFor))
                ForLoop();

            else if (CompareTokenType(TokenTypes.RwBreak))
                Break();

            else if (CompareTokenType(TokenTypes.RwContinue))
                Continue();

            else if (CompareTokenType(TokenTypes.RwStruct))
                Struct();

            else if (CompareTokenType(TokenTypes.Id))
            {
                PreId();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
            }

            else if (CompareTokenType(TokenTypes.RwInclude))
                Include();
            
            else if (CompareTokenType(TokenTypes.RwReturn))
                Return();

            else if (CompareTokenType(TokenTypes.RwSwitch))
                Switch();

            else if (CompareTokenType(TokenTypes.RwEnum))
                Enums();

            else if (CompareTokenType(TokenTypes.RwConst))
                Const();



        }

        private void Const()
        {
            ConsumeNextToken();
       
      
            if (CompareTokenType(TokenTypes.Mul))
                IsPointer();

            if (!CompareTokenType(TokenTypes.Id)) return;
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Asiggnation))
            {
                ValueForId();
                MultiDeclaration();
   
            }
    
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
        }

        private void Return()
        { 
            ConsumeNextToken();

            if (CompareTokenType(TokenTypes.Eos))
                ConsumeNextToken();
            else
            {
                 if(CompareTokenType(TokenTypes.OctalLietral)|| CompareTokenType(TokenTypes.NumericalLiteral) ||  CompareTokenType(TokenTypes.StringLiteral) || CompareTokenType(TokenTypes.DecimalLiteral))
                ConsumeNextToken();

                 else if (CompareTokenType(TokenTypes.Id))
                 { ConsumeNextToken();
                  if( CompareTokenType(TokenTypes.LParenthesis))
                        CallFunction();
                     
                 }

                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
            }


        }

        private void Enums()
        {
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;

            ConsumeNextToken();
            EnumeratorList();

            result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();

            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();

        }

        private void EnumeratorList()
        {
            
            EnumItem();
        }

        private void EnumItem()
        {

            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();

            if (CompareTokenType(TokenTypes.Asiggnation))
                OptionalIndexPosition();

            if (CompareTokenType(TokenTypes.Comma))
            OptionalEnumItem();
        }

        private void OptionalIndexPosition()
        {
            
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.NumericalLiteral, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Comma))
                OptionalEnumItem();

        }

        private void OptionalEnumItem()
        { 
          ConsumeNextToken();
            EnumItem();  

        }

        private void Switch()
        {
            Expression();
            result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
            if (result.Succes)
                throw result.Excpetion;
            ListOfCase();

        }

        private void ListOfCase()
        {
           ConsumeNextToken();
            if(CompareTokenType(TokenTypes.RwCase))
                Case();
            if (CompareTokenType(TokenTypes.RwDefault))
                Default();
        }

        private void Default()
        {
            ConsumeNextToken();
            Expression();
            result = Hanlder.CheckToken(TokenTypes.TwoPoints, _currentToken);
            if (result.Succes)
                throw result.Excpetion;
            ListOfSentences();
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
        }

        private void Case()
        {
           ConsumeNextToken();
            Expression();
            result = Hanlder.CheckToken(TokenTypes.TwoPoints, _currentToken);
            if(result.Succes)
                throw result.Excpetion;
            ListOfSentences();
            if (CompareTokenType(TokenTypes.RwBreak)) 
                    Break();
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
        }

        private void Include()
        {
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.StringLiteral, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();




        }

        private void PreId()
        {
            ConsumeNextToken();
            if(CompareTokenType(TokenTypes.Asiggnation))
            ValueForId();
            else if (CompareTokenType(TokenTypes.LParenthesis))
            {
                CallFunction();
            
            }else
                throw Hanlder.DefaultError(_currentToken);

        }

        private void Struct()
        {
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (result.Succes)
            {
                ConsumeNextToken();

                result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
                if (result.Succes)
                    MemberList();
                else
                    throw result.Excpetion;
           
                result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();

                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();



            }
            else
            {
                throw result.Excpetion;
            }
         

        }

        private void MemberList()
        {
            ConsumeNextToken();
            if (RWords.DataTypes.Contains(_currentToken.Lexeme) || CompareTokenType(TokenTypes.Id))
                DeclaretionOfStruct();

            else
                Hanlder.DefaultError(_currentToken);
            
        } 

        private void DeclaretionOfStruct()
        {
            Gd();
            if (CompareTokenType(TokenTypes.OpenBracket))
            {
                SizeBidArray();

                result = Hanlder.CheckToken(TokenTypes.CloseBracket, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
            }
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
             if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
                 DeclaretionOfStruct();
            }
        }

        

        private void Continue()
        { 
          ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
                ConsumeNextToken();



        }

        private void Break()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Eos)) 
                ConsumeNextToken();

        }

        private void ForLoop()
        {
           ConsumeNextToken();
            if (CompareTokenType(TokenTypes.LParenthesis))
            {
                ForOrForeach();
            }
            else
            {
                Hanlder.DefaultError(_currentToken);
            }

        }

        private void ForOrForeach()
        {
             ConsumeNextToken();
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
                ForEach();
            }
            else
            {
                NormalFor();
            }
        }

        private void NormalFor()
        {
            
            Expression();
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (result.Succes)
            {
                ConsumeNextToken();
                Expression();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (result.Succes)
                {
                    ConsumeNextToken();
                    Expression();
                    result = Hanlder.CheckToken(TokenTypes.RParenthesis, _currentToken);
                    if (result.Succes)
                    {
                        ConsumeNextToken();
                        BlockForLoop();
                    }
                    else
                    {
                        throw result.Excpetion;
                    }
                }
                else
                {
                    throw result.Excpetion;
                }
          
              

            }
            else
            {
                throw result.Excpetion;
            }
        }

        private void ForEach()
        {ConsumeNextToken();

            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (result.Succes)
            {
                result = Hanlder.CheckToken(TokenTypes.TwoPoints, _currentToken);
                if (result.Succes)
                {
                    ConsumeNextToken();
                    result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                    if (result.Succes)
                    {
                        ConsumeNextToken();
                        result = Hanlder.CheckToken(TokenTypes.RParenthesis, _currentToken);
                        if (result.Succes)
                        {
                            BlockForLoop();
                        }
                        else
                        {
                            throw result.Excpetion;
                        }

                    }
                    else
                    {
                        throw result.Excpetion;
                    }
                }
                else
                {
                    throw result.Excpetion;
                }
            }
            else
            {
                throw result.Excpetion;
            }
            
            
        }

        private void While()
        {
            ConsumeNextToken();
            Expression();
            BlockForLoop();

        }

        private void BlockForLoop()
        {
            
            if (CompareTokenType(TokenTypes.Lbrace))
            {
                ConsumeNextToken();
                ListOfSentences();
                if (CompareTokenType(TokenTypes.Rbrace))
                    ConsumeNextToken();
            }
            else
            {
                Hanlder.DefaultError(_currentToken);
            }
                

        }

        private void If()
        {
            ConsumeNextToken();
            Expression();
            BlockForIf();

        }

        private void BlockForIf()
        {
            if (CompareTokenType(TokenTypes.Lbrace))
            {
                ConsumeNextToken();
                ListOfSentences();
               if( CompareTokenType(TokenTypes.Rbrace))
                    ConsumeNextToken();
               if(CompareTokenType(TokenTypes.RwElse))
                    Else();

            }
            else
            {
                Sentence();
                if(CompareTokenType(TokenTypes.RwElse))
                Else();
            }

        }

        private void Else()
        {
            ConsumeNextToken();
            BlockForIf();
        }

        public void Declaretion()
        {
            GeneralDeclaration();
           

        }

        //General declartion is DataTye Pointer Identifeir

        public void Gd()
        {
            ConsumeNextToken();

            if (CompareTokenType(TokenTypes.Mul))
                IsPointer();

            if (CompareTokenType(TokenTypes.Id))
                ConsumeNextToken();
            
            else
                throw new SyntacticException("Expected some Id", _currentToken.Row, _currentToken.Column);
            
        }

        public void GeneralDeclaration()
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
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Asiggnation))
            {
                ValueForId();
                MultiDeclaration();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
            }
            else if (CompareTokenType(TokenTypes.OpenBracket))
            {
                IsArrayDeclaration();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
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
                result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
                ListOfSentences();
                result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();



            }

            else if (CompareTokenType(TokenTypes.Comma))
            {
                MultiDeclaration();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
            }
            else
            {
                Hanlder.DefaultError(_currentToken);
            }

        }

        private void MultiDeclaration()
        {
            OptianalId();
        }

        private void OptianalId()
        {
            if (CompareTokenType(TokenTypes.Comma))
                ListOfId();
        }

        private void ListOfId()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Id))
                OtherIdOrValue();
        }

        private void OtherIdOrValue()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Asiggnation))
                ValueForId();
             if(CompareTokenType(TokenTypes.Comma))
                OptianalId();
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
            }else
            Hanlder.DefaultError(_currentToken);

        }

        private void ChooseIdType()
        {
             
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
            ConsumeNextToken();
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
            if (!RWords.RelationalOperators.ContainsKey(_currentToken.Type) && !CompareTokenType(TokenTypes.Asiggnation))
                return;
            RelationalOperators();
            ExpressionAdicion();
            RelationalExpressionPrime();
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
                ExpressionMul(); // Resolve this problem viendo la gramatica
                ExpressionAdicionPrime();
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
          //  Factor();f

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
            else if (CompareTokenType(TokenTypes.LParenthesis))
            {
                ConsumeNextToken();
                Expression();
                result = Hanlder.CheckToken(TokenTypes.RParenthesis, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;

                ConsumeNextToken();
            }

            //Verificar bien o de literales Booleanas
            else if (CompareTokenType(TokenTypes.NumericalLiteral) || CompareTokenType(TokenTypes.StringLiteral) ||
                     CompareTokenType(TokenTypes.DateLiteral) || CompareTokenType(TokenTypes.NumericalLiteral) ||
                     CompareTokenType(TokenTypes.CharLiteral) || CompareTokenType(TokenTypes.BooleanLiteral) || CompareTokenType(TokenTypes.DecimalLiteral) || CompareTokenType(TokenTypes.OctalLietral))
                     
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
        
            ListOfExpressions();
            if(CompareTokenType(TokenTypes.RParenthesis))
                    ConsumeNextToken();
        }
        

        public void ExpressionUnary()
        {
            if (CompareTokenType(TokenTypes.Increment) || CompareTokenType(TokenTypes.Decrement) ||
                CompareTokenType(TokenTypes.AndBinary) || CompareTokenType(TokenTypes.ComplementBinary) ||
                CompareTokenType(TokenTypes.OrBinary) || CompareTokenType(TokenTypes.XorBinary) ||
                CompareTokenType(TokenTypes.Not) || CompareTokenType(TokenTypes.Sub) || CompareTokenType(TokenTypes.Increment)
                 || CompareTokenType(TokenTypes.Decrement))
                
            {
                ConsumeNextToken();
            }
            Factor();
        }

        public void ListOfExpressions()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.RParenthesis)) return;
            Expression();
            if (CompareTokenType(TokenTypes.Comma))
                ListOfExpressions();
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
