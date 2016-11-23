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
using WebCompilerToNativeC.Tree.Sentences;
using WebCompilerToNativeC.Tree.Sentences.Case;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;
using WebCompilerToNativeC.Tree.Sentences.Enum;
using WebCompilerToNativeC.Tree.Sentences.Fors;
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
            if(CompareTokenType(TokenTypes.Eof) || CompareTokenType(TokenTypes.Rbrace) || CompareTokenType(TokenTypes.RwCase))
                return new List<SentencesNode>();
            var sentences = Sentence();
            var listSentences=     ListOfSentences();
            if(sentences != null)
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
                return null;

            else if(CompareTokenType(TokenTypes.RwElse))
              return  Else();

            
            else if (CompareTokenType(TokenTypes.Html))
                Html();

            else if (CompareTokenType(TokenTypes.RwDo))
              return  DWhile();

            else if (CompareTokenType(TokenTypes.Eos))
                ConsumeNextToken();

            else if (CompareTokenType(TokenTypes.RwIf))
                return If();

            else if (CompareTokenType(TokenTypes.RwWhile))
               return While();

            else if (CompareTokenType(TokenTypes.RwFor))
               return ForLoop();

            else if (CompareTokenType(TokenTypes.RwBreak))
               return Break();

            else if (CompareTokenType(TokenTypes.RwContinue))
               return Continue();

            else if (CompareTokenType(TokenTypes.RwStruct))
               return Struct();

            else if (CompareTokenType(TokenTypes.Id) || CompareTokenType(TokenTypes.Mul))
            {
                var listOfId = new List<PointerNode>();
                if (CompareTokenType(TokenTypes.Mul))
                    IsPointer(listOfId);
                PreId();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
            }

            else if (CompareTokenType(TokenTypes.RwInclude))
              return     Include();
            
            else if (CompareTokenType(TokenTypes.RwReturn))
              return  Return();

            else if (CompareTokenType(TokenTypes.RwSwitch))
             return   Switch();

            else if (CompareTokenType(TokenTypes.RwEnum))
                return   Enums();

            else if (CompareTokenType(TokenTypes.RwConst))
                return Const();

            else
            {
                throw Hanlder.DefaultError(_currentToken);
            }

            return null;
        }

        private SentencesNode DWhile()
        {
            var doWhileExp = new DoWhile {Sentences = BlockForLoop()};
            if (!CompareTokenType(TokenTypes.RwWhile)) throw Hanlder.DefaultError(_currentToken);
            ConsumeNextToken();
            doWhileExp.WhileCondition = Expression();
            if (!CompareTokenType(TokenTypes.Eos)) throw Hanlder.DefaultError(_currentToken);
            ConsumeNextToken();
            return doWhileExp;
        }

        private void Html()
        {
            ConsumeNextToken();
        }

        private SentencesNode Const()
        {
            ConsumeNextToken();

           var constNode = new ConstNode();
            if (Hanlder.DataTypes.ContainsKey(_currentToken.Type))
            {
                constNode.DataType = new IdNode() { Value = _currentToken.Lexeme };
                ConsumeNextToken();
            }
            else
                throw Hanlder.DefaultError(_currentToken);
            var listPointer = new List<PointerNode>();
            if (CompareTokenType(TokenTypes.Mul))
                IsPointer(listPointer);
            constNode.ListOfPointers = listPointer;
            if (CompareTokenType(TokenTypes.Id))
            {
                constNode.Id = new IdNode() {Value = _currentToken.Lexeme};
                ConsumeNextToken();
            }
            else
                throw Hanlder.DefaultError(_currentToken);

            if (CompareTokenType(TokenTypes.Asiggnation))
            {
                ConsumeNextToken();
                var assigmentNode = new Assignation {Value = Expression()};

                constNode.Expression = assigmentNode;
                 ConsumeNextToken();



            }
    
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
            return constNode;
        }

        private SentencesNode Return()
        { 
            ConsumeNextToken();

            if (CompareTokenType(TokenTypes.Eos))
            {
                ConsumeNextToken();
                return new ReturnNode();
            }
            else
            {
              var expReturn=  Expression();
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
                return new ReturnNode() {ExpressionToReturn = expReturn};
               
            }


        }

        private SentencesNode Enums()
        {
            var enumNode = new EnumerationNode();

            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            enumNode.NameOfEnum =  new IdNode() {Value = _currentToken.Lexeme};
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;

            ConsumeNextToken();
            var enumItems = new List<IdNode>();
            EnumeratorList(enumItems);

            result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();

            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
            enumNode.ListEnum = enumItems;
            return enumNode;

        }

        private void EnumeratorList(List<IdNode> listEnum )
        {
            
            EnumItem(listEnum);
        }

        private void EnumItem(List<IdNode> list )
        {
            var newIdNode = new IdNode();
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            newIdNode.Value = _currentToken.Lexeme;
            if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();

          
            if (CompareTokenType(TokenTypes.Asiggnation))
             newIdNode.Assignation=   OptionalIndexPosition();

            list.Add(newIdNode);
            if (CompareTokenType(TokenTypes.Comma))
            OptionalEnumItem(list);
        }

        private Assignation OptionalIndexPosition()
        {
            var assig = new Assignation();
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.NumericalLiteral, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            assig.Value = new IntNode() {Value = _currentToken.Lexeme};
            ConsumeNextToken();
            return assig;
            


        }

        private void OptionalEnumItem(List<IdNode> list)
        { 
          ConsumeNextToken();
            EnumItem(list);  

        }

        private SentencesNode Switch()
        {
            var switchNode = new SwitchNode();
            var listOfStament = new List<CaseStatement>();
           
            ConsumeNextToken();
           switchNode.OptionCase  = Expression();
            result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            Case(listOfStament);
            result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            switchNode.CaseStatements = listOfStament;
            ConsumeNextToken();

            return switchNode;
        }

       

        private CaseStatement Default()
        {
            ConsumeNextToken();
      //      Expression();
            result = Hanlder.CheckToken(TokenTypes.TwoPoints, _currentToken);
            if (result.Succes)
                throw result.Excpetion;
          var listOfSentnces=  ListOfSentences();
            if(CompareTokenType(TokenTypes.Rbrace))
                return new CaseStatement() {Sentences = listOfSentnces};
            throw Hanlder.DefaultError(_currentToken);
        }

        private List<CaseStatement> Case(List<CaseStatement> listOfStatements )
        {

            if (CompareTokenType(TokenTypes.RwCase))
            {
                ConsumeNextToken();
                var expStament = Expression();
                result = Hanlder.CheckToken(TokenTypes.Common, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
                var list = ListOfSentences();
                listOfStatements.Add(new CaseStatement() {CaseOption = expStament, Sentences = list});
                //Review case here
                if (CompareTokenType(TokenTypes.RwBreak))
                    Break();
                if (CompareTokenType(TokenTypes.RwCase))
                    Case(listOfStatements);
                return listOfStatements;

            }else if (CompareTokenType(TokenTypes.RwDefault))
            {

                listOfStatements.Add(Default());
                return listOfStatements;
            }

            return listOfStatements;




        }

        private SentencesNode Include()
        {
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.StringLiteral, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
            var includeNode = new IncludeNode() {LibreryName = _currentToken.Lexeme};
            ConsumeNextToken();
            return includeNode;




        }

        private void PreId()
        {
            ConsumeNextToken();
           
           
                ValueForId();
          

        }

        private SentencesNode Struct()
        {
            var expStruct = new StructNode();

            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (result.Succes)
            {
                expStruct.NameOfStruct = new IdVariable() {Value = _currentToken.Lexeme};
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

        

        private SentencesNode Continue()
        { 
          ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;
                ConsumeNextToken();
            return new ContinueNode();



        }

        private SentencesNode Break()
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Eos)) 
                ConsumeNextToken();
            return new BreakNode();

        }

        private SentencesNode ForLoop()
        {
           ConsumeNextToken();
            if (CompareTokenType(TokenTypes.LParenthesis))
            {
              return  ForOrForeach();
            }
            else
            {
              throw  Hanlder.DefaultError(_currentToken);
            }

        }

        private SentencesNode ForOrForeach()
        {
             ConsumeNextToken();
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
              return  ForEach();
            }
            else
            {
               return NormalFor();
            }
        }

        private SentencesNode NormalFor()
        {
            var normalFor = new ForNode {FirstCondition = Expression()};

            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
            if (result.Succes)
            {
                ConsumeNextToken();
              normalFor.SecondCondition=  Expression();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (result.Succes)
                {
                    ConsumeNextToken();
                   normalFor.ThirdCondition= Expression();
                    result = Hanlder.CheckToken(TokenTypes.RParenthesis, _currentToken);
                    if (result.Succes)
                    {
                        ConsumeNextToken();
                       normalFor.ListStencnesNode.AddRange( BlockForLoop());
                        return normalFor;
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

        private SentencesNode ForEach()

        {
            var forEachNode = new Foreach {DataType = new IdNode() {Value = _currentToken.Lexeme}};
            ConsumeNextToken();
           
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (result.Succes)
            {     forEachNode.Element = new IdNode() {Value = _currentToken.Lexeme};
                ConsumeNextToken();
                result = Hanlder.CheckToken(TokenTypes.Common, _currentToken);
                if (result.Succes)
                {     
                    ConsumeNextToken();
                    result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                    if (result.Succes)
                    {
                        forEachNode.ListToForeach = new IdNode() { Value = _currentToken.Lexeme };
                        ConsumeNextToken();
                        result = Hanlder.CheckToken(TokenTypes.RParenthesis, _currentToken);
                        if (result.Succes)
                        {
                            ConsumeNextToken();
                            forEachNode.ListStencnesNode.AddRange(BlockForLoop());
                            return forEachNode;

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

        private SentencesNode While()
        {
            ConsumeNextToken();
           
            var expWhile =  Expression();
          var listSentences=  BlockForLoop();
            return new WhileNode() {Sentences = listSentences, WhileCondition = expWhile};

        }

        private List<SentencesNode> BlockForLoop()
        {

            if (CompareTokenType(TokenTypes.Lbrace))
            {
                ConsumeNextToken();
                var listExp = ListOfSentences();
                if (CompareTokenType(TokenTypes.Rbrace))
                {
                    ConsumeNextToken();
                    return listExp;
                }
                else
                {
                    throw Hanlder.DefaultError(_currentToken);
                }
            }
            else
            {
                throw Hanlder.DefaultError(_currentToken);
            }
          
                

        }

        private SentencesNode If()
        {
            var ifNode = new IfNode();
            ConsumeNextToken();
            ifNode.IfCondition= Expression();
             var trueAndFalseBlock =BlockForIf();
            ifNode.TrueBlock = trueAndFalseBlock.TrueBlock;
            ifNode.FalseBlock = trueAndFalseBlock.FalseBlock;
            return ifNode;

        }

        private IfNode BlockForIf()
        {
           
            var trueAndFalseBclok = new IfNode();

            if (CompareTokenType(TokenTypes.Lbrace))
            {
                ConsumeNextToken();
                trueAndFalseBclok.TrueBlock= ListOfSentences();
                if (CompareTokenType(TokenTypes.Rbrace))
                {
                    ConsumeNextToken();
                }
                else
                {
                  throw Hanlder.DefaultError(_currentToken);
                }
                if (CompareTokenType(TokenTypes.RwElse))
                {
                    var elseresult = Else();
                    trueAndFalseBclok.FalseBlock.Add(elseresult);
                }
                  
                return trueAndFalseBclok;
            }
            else
            {
               trueAndFalseBclok.TrueBlock.Add(Sentence());
                if(CompareTokenType(TokenTypes.RwElse))
              trueAndFalseBclok.FalseBlock.Add(Else());
                return trueAndFalseBclok;
                
            }

        }

        private SentencesNode Else()
        {
            ConsumeNextToken();
          return  BlockForIf();
        }

        public void Declaretion()
        {
            GeneralDeclaration();
           

        }

        //General declartion is DataTye Pointer Identifeir

        public void Gd()
        {
            var listOfPointer = new List<PointerNode>();
            ConsumeNextToken();

            if (CompareTokenType(TokenTypes.Mul))  
                IsPointer(listOfPointer);

            if (CompareTokenType(TokenTypes.Id))
                ConsumeNextToken();
            
            else
                throw new SyntacticException("Expected some Id", _currentToken.Row, _currentToken.Column);
            
        }

        public void GeneralDeclaration()
        {
            ConsumeNextToken();
            var listOfPointer = new List<PointerNode>();
            if (CompareTokenType(TokenTypes.Mul))
            IsPointer(listOfPointer);

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
                var listOfPointer = new List<PointerNode>();
                IsPointer(listOfPointer);
                
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
                OptionalInitOfArray(null);
            
            else
            {
                
            }
        }

        private void OptionalInitOfArray(List<ExpressionNode> listExpression )
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
          IdAccesorsOrFunction(null);
           
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
              var exp=  Expression();
                result = Hanlder.CheckToken(TokenTypes.RParenthesis, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;

                ConsumeNextToken();
                return exp;
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
                CallFunction(null);
            

    
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
            ListOfExpressions(null);

        }

        public void IsPointer(List<PointerNode> listPointer )
        {
            
            listPointer.Add(new PointerNode());
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Mul))
                IsPointer(listPointer);




        }
    }

   
}
