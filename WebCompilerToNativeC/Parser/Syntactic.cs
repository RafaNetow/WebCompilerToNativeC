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
using WebCompilerToNativeC.Tree;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.DataType.BaseClass;
using WebCompilerToNativeC.Tree.DataType.Boolean;
using WebCompilerToNativeC.Tree.DataType.IdNode;
using WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre;
using WebCompilerToNativeC.Tree.UnaryNode;

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


        public List<SentencesNode> Parse()
        {
           var sentencesList =  ListOfSentences();
            return sentencesList;
        }
        /***************************/

        public List<SentencesNode> ListOfSentences()
        {
            if(CompareTokenType(TokenTypes.Eof) && CompareTokenType(TokenTypes.Rbrace))
                return new List<SentencesNode>();
            var sentences = Sentence();
            var listSentences=     ListOfSentences();
            listSentences.Insert(0,sentences);
            return listSentences;
           
        }

       

        public SentencesNode Sentence()
        {
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
                Declaretion();
              
            }
            else if (CompareTokenType(TokenTypes.Rbrace))
                return ;

            else if(CompareTokenType(TokenTypes.RwElse))
                Else();

            else if (CompareTokenType(TokenTypes.Html))
                Html();

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

            else if(CompareTokenType(TokenTypes.RwCase))
                Case();

            else if (CompareTokenType(TokenTypes.Id) || CompareTokenType(TokenTypes.Mul))
            {
                if(CompareTokenType(TokenTypes.Mul))
                    IsPointer();
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

        private void Html()
        {
            ConsumeNextToken();
        }

        private void Const()
        {
            ConsumeNextToken();
       
      
            if (CompareTokenType(TokenTypes.Mul))
                IsPointer();

            if (!CompareTokenType(TokenTypes.Id)) return;
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Asiggnation) || CompareTokenType(TokenTypes.AddAndAssignment) || CompareTokenType(TokenTypes.SubAndAssignment) || CompareTokenType(TokenTypes.MulAndAssignment) || CompareTokenType(TokenTypes.DivAndAssignment))
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
            {     Expression();
                // if(CompareTokenType(TokenTypes.OctalLietral)|| CompareTokenType(TokenTypes.NumericalLiteral) ||  CompareTokenType(TokenTypes.StringLiteral) || CompareTokenType(TokenTypes.DecimalLiteral))
                //ConsumeNextToken();

                // else if (CompareTokenType(TokenTypes.Id))
                // { ConsumeNextToken();
                //  if( CompareTokenType(TokenTypes.LParenthesis))
                //        CallFunction();
                     
                // }

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
            ConsumeNextToken();
            Expression();
            result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ListOfCase();
            result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
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
            result = Hanlder.CheckToken(TokenTypes.Common, _currentToken);
            if(!result.Succes)
                throw result.Excpetion;
             ConsumeNextToken();
            ListOfSentences();
            //Review case here
            if (CompareTokenType(TokenTypes.RwBreak)) 
                    Break();
          
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
           
           
                ValueForId();
          

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
                ConsumeNextToken();
                result = Hanlder.CheckToken(TokenTypes.Common, _currentToken);
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
            if (CompareTokenType(TokenTypes.Asiggnation) || CompareTokenType(TokenTypes.AddAndAssignment) || CompareTokenType(TokenTypes.SubAndAssignment) || CompareTokenType(TokenTypes.MulAndAssignment) || CompareTokenType(TokenTypes.DivAndAssignment))
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
               
                if (CompareTokenType(TokenTypes.Comma))
                {
                    ConsumeNextToken();
                    TypeOfDeclaration();
                }
                else
                {
                    result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                    if (!result.Succes)
                        throw result.Excpetion;
                }

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
            if (CompareTokenType(TokenTypes.Asiggnation) || CompareTokenType(TokenTypes.AddAndAssignment) || CompareTokenType(TokenTypes.SubAndAssignment) || CompareTokenType(TokenTypes.MulAndAssignment) || CompareTokenType(TokenTypes.DivAndAssignment))
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

        private ExpressionNode OptionalInitOfArray(List<ExpressionNode> listExpression )
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Lbrace))
            {
                ListOfExpressions(listExpression);
                if (CompareTokenType(TokenTypes.Rbrace))
                    ConsumeNextToken();
            }
            else
            {
                throw new SyntacticException("Expected a  ;", _currentToken.Row, _currentToken.Column);
            }
        }

        private ExpressionNode BidArray()
        {
            ConsumeNextToken();
          var bidAccesor =   SizeBidArray();
            if (CompareTokenType(TokenTypes.CloseBracket))
            {
                ConsumeNextToken();
                return bidAccesor;
            }
            else
                throw new SyntacticException("Expected a CloseBracket", _currentToken.Row, _currentToken.Column);
        }

        private ExpressionNode SizeBidArray()
        {
           
            if (Hanlder.LiteralWithDecreOrIncre.ContainsKey(_currentToken.Type))
                return new ArrayAccesorNode() { Value = Hanlder.LiteralWithDecreOrIncre[_currentToken.Type] };

            if (CompareTokenType(TokenTypes.Id))
                return new ArrayAccesorNode() { Value = new IdVariable() { Value = _currentToken.Lexeme } };

            throw new SyntacticException("Do not can initializer an array with the type of identifeir", _currentToken.Row, _currentToken.Column);

        }

        private ExpressionNode SizeForArray()
        {
            if (CompareTokenType(TokenTypes.RParenthesis))
            {
                return new ArrayAccesorNode();
            }
            if(Hanlder.LiteralWithDecreOrIncre.ContainsKey(_currentToken.Type))
                return new ArrayAccesorNode() {Value = Hanlder.LiteralWithDecreOrIncre[_currentToken.Type]};

            if(CompareTokenType(TokenTypes.Id))
                return new ArrayAccesorNode() {Value = new IdVariable() {Value = _currentToken.Lexeme} };

            throw new SyntacticException("Do not can initializer an array with the type of identifeir", _currentToken.Row, _currentToken.Column);
           


        }

        public ExpressionNode IdAccesorsOrFunction(IdVariable currentIdVariable)
        {
            if (CompareTokenType(TokenTypes.LParenthesis))
            {
               return CallFunction(currentIdVariable.Value);

            }
            else
            {
                if (CompareTokenType(TokenTypes.OpenBracket))
                {
                    ConsumeNextToken();                   
                   var accesor =   SizeForArray();
                   currentIdVariable.Accesors.Add((AccesorNode) accesor);
                    ConsumeNextToken();
                    if (CompareTokenType(TokenTypes.OpenBracket))
                    {
                     var bidAccesor =    BidArray();
                        currentIdVariable.Accesors.Add((AccesorNode) bidAccesor);
                    }
                }        
                   var accesorArrowOrDot =     ArrowOrDot();
                    currentIdVariable.Accesors.Add((AccesorNode) accesorArrowOrDot);
                return currentIdVariable;


            }
        }


        private void ValueForId()
        {
          IdAccesorsOrFunction();
           
            if (CompareTokenType(TokenTypes.Asiggnation) || CompareTokenType(TokenTypes.AddAndAssignment) ||
                CompareTokenType(TokenTypes.SubAndAssignment) || CompareTokenType(TokenTypes.MulAndAssignment) ||
                CompareTokenType(TokenTypes.DivAndAssignment))

            {ConsumeNextToken();
                Expression();
            }






        }

        private ExpressionNode ArrowOrDot()
        {

            if (CompareTokenType(TokenTypes.Point))
            {ConsumeNextToken();

                result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;

                var idVal = _currentToken.Lexeme;
                ConsumeNextToken();
                return new PropertyAccesorNode() {Id = new IdNode() {Value = _currentToken.Lexeme} };
            }

            if (CompareTokenType(TokenTypes.reference))
            {
                ConsumeNextToken();

                result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;

                var idVal = _currentToken.Lexeme;
                ConsumeNextToken();
                return new PropertyAccesorNode() { Id = new IdNode() { Value = _currentToken.Lexeme } };
            }


            return new PropertyAccesorNode();

              
          

        }

        private ExpressionNode Expression()
        {
           return  RelationalExpression();
        

        }


        //Compare if exist some retalacion operation that should return some bool or some similar
        private ExpressionNode RelationalExpressionPrime(ExpressionNode param)
        {
            if (!Hanlder.RelationalOp.ContainsKey(_currentToken.Type))
                return param;
 
         var expRelationOp =    Hanlder.RelationalOp[_currentToken.Type]; 
            RelationalOperators();
        var  expAddOp=   ExpressionAdicion();

            expRelationOp.LeftOperand = param;
            expRelationOp.RightOperand = expAddOp;
            return RelationalExpressionPrime(expRelationOp);
        }

        private void RelationalOperators()
        {
           ConsumeNextToken();
        }
       //4
        public ExpressionNode RelationalExpression()
        {
         var expAdition =     ExpressionAdicion();
        return    RelationalExpressionPrime(expAdition);

        }

        private ExpressionNode ExpressionAdicion()
        {

            var mulVal =ExpressionMul();
           return  ExpressionAdicionPrime(mulVal);
        }

        public ExpressionNode ExpressionAdicionPrime(ExpressionNode mulVal)
        {
            if (!Hanlder.AdditionOp.ContainsKey(_currentToken.Type)) return mulVal;
            var additionOp = Hanlder.AdditionOp[_currentToken.Type];
            additionOp.LeftOperand = mulVal;
            AdditiveOperators();
            additionOp.RightOperand =  ExpressionMul(); // Resolve this problem viendo la gramatica

                  
           return ExpressionAdicionPrime(additionOp);

         

        }

        private void AdditiveOperators()
        {
            ConsumeNextToken();

        }

        private ExpressionNode ExpressionMul()
        {
            
              var fValue =  ExpressionUnary();
              return  ExpressionMulPrime(fValue);
           // }
          //  Factor();f

        }

        private ExpressionNode ExpressionMulPrime(ExpressionNode param)
        {
            if (!Hanlder.OperatorsMul.ContainsKey(_currentToken.Type)) return param;
            var operatorMul = Hanlder.OperatorsMul[_currentToken.Type];
            operatorMul.LeftOperand = param;                
            ConsumeNextToken();
            var fvalue = ExpressionUnary();
            operatorMul.RightOperand = fvalue;
            return  ExpressionMulPrime(operatorMul);
        }

        public ExpressionNode Factor()
        {




            if (Hanlder.DataTypes.ContainsKey(_currentToken.Type))
            {
              var currentDataType =  Hanlder.DataTypes[_currentToken.Type];
                currentDataType.Value = _currentToken.Lexeme;
                ConsumeNextToken();

                if (Hanlder.LiteralWithDecreOrIncre.ContainsKey(_currentToken.Type))
                {
                    OptionalIncrementOrDecrement((LiteralWithOptionalIncrementOrDecrement) currentDataType);
                }
                return currentDataType;
            }

       
            else if (CompareTokenType(TokenTypes.Id))
            {  
               var iVariable = new IdVariable() {Value = _currentToken.Lexeme};
               ConsumeNextToken();

                if (CompareTokenType(TokenTypes.Increment) || CompareTokenType(TokenTypes.Decrement))
                {
                    if (CompareTokenType(TokenTypes.Increment))
                        iVariable.IncrementOrDecrement = new RightIncrement();

                    else if (CompareTokenType(TokenTypes.Decrement))
                        iVariable.IncrementOrDecrement = new LeftIncrement();
                }
                else
                {
                  var id =    IdAccesorsOrFunction(iVariable);
                    return id;
                }
                
              
                 
            }
           
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
            
            else
            {
                throw new SyntacticException("Expected a factor", _currentToken.Row, _currentToken.Column);
            }
            return null;
        }

        private ExpressionNode OptionalIncrementOrDecrement(LiteralWithOptionalIncrementOrDecrement idExpressionNode)
        {
            if(CompareTokenType(TokenTypes.Increment))
                idExpressionNode.DecremmentOrIncremment = new RightIncrement();
            
           else if(CompareTokenType(TokenTypes.Decrement))
                idExpressionNode.DecremmentOrIncremment = new LeftIncrement();

                ConsumeNextToken();
            return idExpressionNode;

        }

        /* In this production could be three case
        *CallFunction
        *Index,Pointer or Some Point to Acces  */
        public void FactorFunArray()
        {

       
           
           
            if (CompareTokenType(TokenTypes.LParenthesis))
                CallFunction();
            

    
        }

        public ExpressionNode CallFunction(string nameOfFunction)
        {
           var listOfExpression = new List<ExpressionNode>();
                ListOfExpressions(listOfExpression);


            if (!CompareTokenType(TokenTypes.RParenthesis)) throw Hanlder.DefaultError(_currentToken);
            ConsumeNextToken();
            return new CallFunction() {NameOfFunction = nameOfFunction, ListOfExpression = listOfExpression};
        }
        

        public ExpressionNode ExpressionUnary()
        {
           var unaryExp= new ExpressionUnaryNode();
          
            if( Hanlder.UnariesNode.ContainsKey(_currentToken.Type))

            {
                unaryExp.UnaryOperator = Hanlder.UnariesNode[_currentToken.Type];
                ConsumeNextToken();
            }
         var factorExpression =    Factor();
            unaryExp.Factor = factorExpression;
            return unaryExp;
        }

        public void ListOfExpressions(List<ExpressionNode> listOfExpression )
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.RParenthesis)) return;
        var exp =     Expression();
            listOfExpression.Add(exp);
            if (CompareTokenType(TokenTypes.Comma))
                ListOfExpressions(listOfExpression);
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
