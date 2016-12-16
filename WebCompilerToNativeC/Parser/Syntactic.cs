using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
using WebCompilerToNativeC.Tree.DataType.IdNode.Accesors;
using WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre;
using WebCompilerToNativeC.Tree.DataType.Struct;
using WebCompilerToNativeC.Tree.Sentences;
using WebCompilerToNativeC.Tree.Sentences.Case;
using WebCompilerToNativeC.Tree.Sentences.Declaretion;
using WebCompilerToNativeC.Tree.Sentences.Enum;
using WebCompilerToNativeC.Tree.Sentences.Fors;
using WebCompilerToNativeC.Tree.Sentences;
using WebCompilerToNativeC.Tree.Sentences.Declaretion.ArrayWithInialiation;
using WebCompilerToNativeC.Tree.Sentences.Structs;
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
               return Declaretion();
              
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
               var preid =PreId();
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)

                    throw result.Excpetion;
                ConsumeNextToken();
                return preid;
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
            ConsumeNextToken();
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
            if (Hanlder.DataTypesLexeme.ContainsKey(_currentToken.Lexeme))
            {
                constNode.DataType = Hanlder.DataTypesLexeme[_currentToken.Lexeme];
                constNode.DataType.Value = _currentToken.Lexeme;
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

                // else if (CompareTokenType(TokenTypes.id))
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

        


        private SentencesNode PreId()
        {

            var functionOrAssigment = new CallFunctionOrAssignment() { IdToAssignment = new IdVariable() { Value = _currentToken.Lexeme } };
            var listOfAccesors = new List<AccesorNode>();
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.OpenBracket))
            {
              
                IsArrayDeclaration(listOfAccesors);

                 result = Hanlder.CheckToken(TokenTypes.CloseBracket, _currentToken);
               
             
            }
            if(Hanlder.TypeOfAccesors.ContainsKey(_currentToken.Type))
            ArrowOrDot(listOfAccesors);


              functionOrAssigment.IdToAssignment.Accesors = listOfAccesors;           
               functionOrAssigment.Expression=  ValueForId(functionOrAssigment.IdToAssignment);
          
            return functionOrAssigment;
        }

        private SentencesNode Struct()
        {
            var expStruct = new StructNode();
            var lisOfStructDeclaretion = new List<DeclarationNode>();

            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (result.Succes)
            {
                expStruct.NameOfStruct = new IdVariable() {Value = _currentToken.Lexeme};
                ConsumeNextToken();

                result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
                if (result.Succes)
                    MemberList(lisOfStructDeclaretion);
                else
                    throw result.Excpetion;
                expStruct.StructItems = lisOfStructDeclaretion;
                result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
                var listOfVariableDeclaretion = new List<IdVariable>();
                OptianalVariableStruct(listOfVariableDeclaretion);
                expStruct.VariableDeclaretion = listOfVariableDeclaretion;
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();

             

                return expStruct;
            }
            else
            {
                throw result.Excpetion;
            }
         

        }

        private void OptianalVariableStruct(List<IdVariable> idVariables)
        {
            if(CompareTokenType(TokenTypes.Id))
                idVariables.Add(new IdVariable() {Value = _currentToken.Lexeme});
            ConsumeNextToken();

            if (!CompareTokenType(TokenTypes.Comma)) return;
            ConsumeNextToken();
            OptianalVariableStruct(idVariables);
        }

        private void MemberList(List<DeclarationNode> lisOfStructDeclaretion)
        {
            ConsumeNextToken();
            if (RWords.DataTypes.Contains(_currentToken.Lexeme) || CompareTokenType(TokenTypes.Id))
                DeclaretionOfStruct(lisOfStructDeclaretion);

            else
                Hanlder.DefaultError(_currentToken);
            
        } 

        private void DeclaretionOfStruct(List<DeclarationNode> lisOfStructDeclaretion)
        {
            Gd(lisOfStructDeclaretion);
         
            result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
             if (!result.Succes)
                throw result.Excpetion;
            ConsumeNextToken();
            if (RWords.DataTypes.Contains(_currentToken.Lexeme))
            {
                 DeclaretionOfStruct(lisOfStructDeclaretion);
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
            var variable = new IdVariable() {Value = _currentToken.Lexeme};
            ConsumeNextToken();
           variable= (IdVariable) ValueForId(variable);
            var normalFor = new ForNode {FirstCondition = variable};

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

        public SentencesNode Declaretion()
        {
           return  GeneralDeclaration();
           

        }

        //General declartion is DataTye Pointer Identifeir

        public void Gd(List<DeclarationNode> lisOfStructDeclaretion)
        {
            var newDeclaretion = new DeclarationNode();
            
            var listOfPointer = new List<PointerNode>();
            if (CompareTokenType(TokenTypes.Id))
                newDeclaretion.TypeStructOrEnum = _currentToken.Lexeme;
            else
                newDeclaretion.Type = Hanlder.DataTypesLexeme[_currentToken.Lexeme];
                
            
            ConsumeNextToken();

            if (CompareTokenType(TokenTypes.Mul))  
                IsPointer(listOfPointer);

            newDeclaretion.ListOfPointers = listOfPointer;

            if (CompareTokenType(TokenTypes.Id))
            {
                newDeclaretion.Variable = new IdVariable() {Value = _currentToken.Lexeme};
                ConsumeNextToken();
            }
            else
                throw new SyntacticException("Expected some id", _currentToken.Row, _currentToken.Column);

            if (CompareTokenType(TokenTypes.OpenBracket))
            {
                var listOfAccesNode = new List<AccesorNode>();
                IsArrayDeclaration(listOfAccesNode);
                newDeclaretion.Variable.Accesors = listOfAccesNode;

            } 
            lisOfStructDeclaretion.Add(newDeclaretion);
        }

        public SentencesNode GeneralDeclaration()
        {
            var declaretion = new DeclarationNode {Type = Hanlder.DataTypesLexeme[_currentToken.Lexeme]};

            ConsumeNextToken();

            var listOfPointer = new List<PointerNode>();
                
            if (CompareTokenType(TokenTypes.Mul))
            IsPointer(listOfPointer);

            if (CompareTokenType(TokenTypes.Id))
            {
                declaretion.ListOfPointers = listOfPointer;
                declaretion.Variable = new IdVariable() {Value = _currentToken.Lexeme};

                return   TypeOfDeclaration(declaretion);
            }
            else
            {
                throw new SyntacticException("Expected some id", _currentToken.Row, _currentToken.Column);
            }
           
        }

        public SentencesNode TypeOfDeclaration(DeclarationNode generalDeclaretion)
        {
            ConsumeNextToken();
            if (Hanlder.AssignationOperator.ContainsKey(_currentToken.Type))
            {

                generalDeclaretion.Variable= (IdVariable) ValueForId(generalDeclaretion.Variable);

                var newVariable = new IdVariable(generalDeclaretion.Variable.Value, generalDeclaretion.Variable.IncrementOrDecrement, generalDeclaretion.Variable.TypeOfAssignment, generalDeclaretion.Variable.Accesors, generalDeclaretion.Variable.ValueOfAssigment);
                var listIdNode = new List<IdVariable>();
                MultiDeclaration(listIdNode);
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);

                if (!result.Succes)
                    throw result.Excpetion;

                ConsumeNextToken();
                var declaretionPosition = new Token();
                declaretionPosition = _currentToken;
                 var simpleDeclaretion = new MultiDeclaration() {SentencesPosition = declaretionPosition, Type = generalDeclaretion.Type, Variable = generalDeclaretion.Variable, ListOfPointers  = generalDeclaretion.ListOfPointers,ListOfIdNodes = listIdNode};
                return simpleDeclaretion;
            }
            else if (CompareTokenType(TokenTypes.OpenBracket))
            {
                var listOfAcces = new List<AccesorNode>();
                IsArrayDeclaration(listOfAcces);
                generalDeclaretion.Variable.Accesors = listOfAcces;
               
                if (CompareTokenType(TokenTypes.Asiggnation))
                {

                    var listOfValue = new List<ExpressionNode>();
                    OptionalInitOfArray(listOfValue);

                    return new ArrayWithInitialization()  {InitValues = listOfValue,Type = generalDeclaretion.Type, Variable = generalDeclaretion.Variable, ListOfPointers = generalDeclaretion.ListOfPointers};
                  
                }
                else
                {
                    result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                    if (!result.Succes)
                        throw result.Excpetion;
                    var declaretionPosition = new Token();
                    declaretionPosition = _currentToken;
                    generalDeclaretion.SentencesPosition = declaretionPosition;
                    return generalDeclaretion;
                }
                return null;
              
            }
            else if (CompareTokenType(TokenTypes.Eos))
            {
                ConsumeNextToken();
                return generalDeclaretion;
            }
            else if (CompareTokenType(TokenTypes.LParenthesis))
            {
                var functioNDeclaretion = new FunctionDeclaretion();
                ConsumeNextToken();
                var functionDeclaretionList = new List<DeclarationNode>();
                ParameterList(functionDeclaretionList);
                result = Hanlder.CheckToken(TokenTypes.RParenthesis, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
                result = Hanlder.CheckToken(TokenTypes.Lbrace, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
               functioNDeclaretion.ListOfEspecialSentences=  ListOfSentences();
                result = Hanlder.CheckToken(TokenTypes.Rbrace, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();

                functioNDeclaretion.ParameterList = functionDeclaretionList;
                functioNDeclaretion.Type = generalDeclaretion.Type;
                functioNDeclaretion.ListOfPointers = generalDeclaretion.ListOfPointers;
                functioNDeclaretion.Variable = generalDeclaretion.Variable;
                var functionPostion = new Token();
                functionPostion = _currentToken;
                functioNDeclaretion.SentencesPosition = functionPostion;
                return functioNDeclaretion;
            }

            else if (CompareTokenType(TokenTypes.Comma))
            {
                var listIdNode = new List<IdVariable>();
                MultiDeclaration(listIdNode);
                result = Hanlder.CheckToken(TokenTypes.Eos, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                ConsumeNextToken();
                return null;
            }
            else
            {
              throw  Hanlder.DefaultError(_currentToken);
            }

            
        }

        private void MultiDeclaration(List<IdVariable> listid )
        {
            OptianalId(listid);
        }

        private void OptianalId(List<IdVariable> listId)
        {
            if (CompareTokenType(TokenTypes.Comma))
                ListOfId(listId);
        }

        private void ListOfId(List<IdVariable> listId)
        {
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.Id))
                OtherIdOrValue(listId);
        }

        private void OtherIdOrValue(List<IdVariable> listId)
        {
            var newVariable = new IdVariable {Value = _currentToken.Lexeme};

            ConsumeNextToken();
            if (Hanlder.AssignationOperator.ContainsKey(_currentToken.Type))
            {
              newVariable=  (IdVariable) ValueForId(newVariable);
                listId.Add(newVariable);
            }
            if (CompareTokenType(TokenTypes.Comma))
            {
               listId.Add(newVariable);
                OptianalId(listId);
            }
               

            else if (CompareTokenType(TokenTypes.OpenBracket))
            {
                List<AccesorNode> listOfAcces = new List<AccesorNode>();
                IsArrayDeclaration(listOfAcces);
                newVariable.Accesors = listOfAcces;
                listId.Add(newVariable);
                if (CompareTokenType(TokenTypes.Comma))
                {

                    OptianalId(listId);
                }
                else
                {
                    //result = Hanlder.CheckToken(TokenTypes.CloseBracket, _currentToken);
                    //if (!result.Succes)
                    //    throw result.Excpetion;
                    //ConsumeNextToken();
                    if (CompareTokenType(TokenTypes.Comma))
                        OptianalId(listId);
                }

            }
            else
            {
                listId.Add(newVariable);
            }

        }

        private void ParameterList(List<DeclarationNode> listOfDeclaretion)
        {
            if (Hanlder.DataTypesLexeme.ContainsKey(_currentToken.Lexeme))
            {
                listOfDeclaretion.Add(ChooseIdType( Hanlder.DataTypesLexeme[_currentToken.Lexeme]));
                if (CompareTokenType(TokenTypes.Comma))
                {
                    OptionalListOfParams(listOfDeclaretion);
                }
                else
                {
                   
                }
            }
            else
            {
                
            }
            

        }

        private void OptionalListOfParams(List<DeclarationNode> listOfDeclaretion)
        {
           ConsumeNextToken();
            if (Hanlder.DataTypesLexeme.ContainsKey(_currentToken.Lexeme))
            {
               listOfDeclaretion.Add(ChooseIdType(Hanlder.DataTypesLexeme[_currentToken.Lexeme]));
                if (CompareTokenType(TokenTypes.Comma))
                {
                    OptionalListOfParams(listOfDeclaretion);
                }
                else
                {
                  
                }
            }else
            Hanlder.DefaultError(_currentToken);

        }

        private DeclarationNode ChooseIdType(DataType type)
        {
             var newDeclaretion = new DeclarationNode();
            //Revisar si esta parte del codigo va a funcionar
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.AndBinary))            
            {   ConsumeNextToken();
                
               result=  Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                if (result.Succes)
                {
                    newDeclaretion.Type = type;
                    newDeclaretion.Variable = new IdVariable() {Value = _currentToken.Lexeme};
                    ConsumeNextToken();
                    var declaretionPostion = new Token();
                    declaretionPostion = _currentToken;
                    newDeclaretion.SentencesPosition = declaretionPostion;
                    return newDeclaretion;
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
                    newDeclaretion.Type = type;
                    newDeclaretion.Variable = new IdVariable() { Value = _currentToken.Lexeme };
                    newDeclaretion.ListOfPointers = listOfPointer;
                    ConsumeNextToken();
                    var declaretionPostion = new Token();
                    declaretionPostion = _currentToken;
                    newDeclaretion.SentencesPosition = declaretionPostion;
                    return newDeclaretion;
                }
                else
                {
                    throw result.Excpetion;
                }
            }else if (CompareTokenType(TokenTypes.Id))
            {
                newDeclaretion.Type = type;
                newDeclaretion.Variable = new IdVariable() { Value = _currentToken.Lexeme };
                ConsumeNextToken();
                var declaretionPostion = new Token();
                declaretionPostion = _currentToken;
                newDeclaretion.SentencesPosition = declaretionPostion;
                return newDeclaretion;
            }
            else
            {
                Hanlder.DefaultError(_currentToken);
            }


            return null;
        }

        private void IsArrayDeclaration(List<AccesorNode> listOfAccces )
        { ConsumeNextToken();
          listOfAccces.Add( (AccesorNode) SizeForArray());
         

            result = Hanlder.CheckToken(TokenTypes.CloseBracket, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;

            ConsumeNextToken();

            if (CompareTokenType(TokenTypes.OpenBracket))
             listOfAccces.Add( (AccesorNode) BidArray());

            //if (CompareTokenType(TokenTypes.Asiggnation))
            //   OptionalInitOfArray(null);
            
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
                var expressionPostion = new Token();
                expressionPostion = _currentToken;
                bidAccesor.NodePosition = expressionPostion;
                return bidAccesor;
            }
            else
                throw new SyntacticException("Expected a CloseBracket", _currentToken.Row, _currentToken.Column);
        }

        private ExpressionNode SizeBidArray()
        {

            if (Hanlder.LiteralWithDecreOrIncre.ContainsKey(_currentToken.Type))
            {
                var value = Hanlder.LiteralWithDecreOrIncre[_currentToken.Type];
                value.Value = _currentToken.Lexeme;
                ConsumeNextToken();
                var expressionPostion = new Token();
                expressionPostion = _currentToken;
                var arrayPropertie = new ArrayAccesorNode
                {
                    Value = value,
                    NodePosition = expressionPostion
                };
                return arrayPropertie;
                
            }

            if (CompareTokenType(TokenTypes.Id))
            {
                var currentVariable = new IdVariable() { Value = _currentToken.Lexeme };
                ConsumeNextToken();
                IdProperties(currentVariable);
                var expressionPostion = new Token();
                expressionPostion = _currentToken;
                var arrayPropertie = new ArrayAccesorNode
                {
                    Value = currentVariable,
                    NodePosition = expressionPostion
                };
                return arrayPropertie;

            }

            throw new SyntacticException("Do not can initializer an array with the type of identifeir", _currentToken.Row, _currentToken.Column);

        }

        private ExpressionNode SizeForArray()
        {
            if (CompareTokenType(TokenTypes.CloseBracket))
            {
                return new ArrayAccesorNode();
            }
            if (Hanlder.LiteralWithDecreOrIncre.ContainsKey(_currentToken.Type))
            {
                var value = Hanlder.LiteralWithDecreOrIncre[_currentToken.Type];
                value.Value = _currentToken.Lexeme;
                ConsumeNextToken();
                var expressionPostion = new Token();
                expressionPostion = _currentToken;
                var arrayPropertie = new ArrayAccesorNode
                {
                    Value = value,
                    NodePosition = expressionPostion
                };
                return arrayPropertie;
              
            }

            if (CompareTokenType(TokenTypes.Id))
            {
                var currentVariable = new IdVariable() {Value = _currentToken.Lexeme};
                
                
                    IdProperties(currentVariable);

                var expressionPostion = new Token();
                expressionPostion = _currentToken;
                var arrayPropertie = new ArrayAccesorNode
                {
                    Value = currentVariable,
                    NodePosition = expressionPostion
                };
                return arrayPropertie;
              
            }
          

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
              return   IdProperties(currentIdVariable);

                  
                      
              

            }
        }


        public ExpressionNode IdProperties(IdVariable id)
        {
            if(CompareTokenType(TokenTypes.Id))
                ConsumeNextToken();
            var listOfAccesors = new List<AccesorNode>();
            GetAllAccesorNodes(listOfAccesors);

            if (id.Accesors != null) id.Accesors = listOfAccesors;
            var expressionPostion = new Token();
            expressionPostion = _currentToken;

            id.NodePosition = expressionPostion;
            return id;
        }





      public  void  GetArrayProperties(List<AccesorNode> listOfAccesor)
        {
            ConsumeNextToken();
            listOfAccesor.Add((AccesorNode)SizeForArray());
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.OpenBracket))
                listOfAccesor.Add((AccesorNode)BidArray());

        }

        private void GetAllAccesorNodes(List<AccesorNode> listOfAccesors)
        {
            if (CompareTokenType(TokenTypes.OpenBracket)) 
            {      GetArrayProperties(listOfAccesors);
                if (Hanlder.TypeOfAccesors.ContainsKey(_currentToken.Type))
                    ArrowOrDot(listOfAccesors);

            }
            else
            {
                if (Hanlder.TypeOfAccesors.ContainsKey(_currentToken.Type))
                    ArrowOrDot(listOfAccesors);
            }
        }


        public ExpressionNode ValueForId(IdVariable currentId)
        {
            if (!Hanlder.AssignationOperator.ContainsKey(_currentToken.Type))
            {
                return currentId;
            }

            currentId.TypeOfAssignment = Hanlder.AssignationOperator[_currentToken.Type];
            if(  currentId.TypeOfAssignment is AssignationBinary)
                 currentId.TypeOfAssignment = new AssignationBinary();
            ConsumeNextToken();
            currentId.ValueOfAssigment =  Expression();
            return currentId;
        }

        private void ArrowOrDot(List<AccesorNode> listOfAccesors)
        {
                    
            if (CompareTokenType(TokenTypes.Point))
            {ConsumeNextToken();
                result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
                if (!result.Succes)
                    throw result.Excpetion;
                listOfAccesors.Add(new PropertyAccesorNode() { Id = new IdNode() { Value = _currentToken.Lexeme } });
                ConsumeNextToken();
                if (CompareTokenType(TokenTypes.OpenBracket))
                  IsArrayDeclaration(listOfAccesors);


                if (Hanlder.TypeOfAccesors.ContainsKey(_currentToken.Type))
                    ArrowOrDot(listOfAccesors);


                


             


              
                
            
               
            }

            if (!CompareTokenType(TokenTypes.reference)) return;
            ConsumeNextToken();
            result = Hanlder.CheckToken(TokenTypes.Id, _currentToken);
            if (!result.Succes)
                throw result.Excpetion;

            listOfAccesors.Add(new ReferenceNode() {Id = new IdNode() {Value = _currentToken.Lexeme} });
            ConsumeNextToken();
            if (CompareTokenType(TokenTypes.OpenBracket))
                IsArrayDeclaration(listOfAccesors);

            if (Hanlder.TypeOfAccesors.ContainsKey(_currentToken.Type))
                ArrowOrDot(listOfAccesors);





        }

        private ExpressionNode Expression()
        {

             var expresionPosition = new Token();
            expresionPosition = _currentToken;
          var relationNode=  RelationalExpression();
            relationNode.NodePosition = expresionPosition;


            return relationNode;
        

        }


        //Compare if exist some retalacion operation that should return some bool or some similar
        private ExpressionNode RelationalExpressionPrime(ExpressionNode param)
        {
            if (!Hanlder.RelationalOp.ContainsKey(_currentToken.Type))
            {
                var expresionPosition = new Token();
                expresionPosition = _currentToken;
               
                param.NodePosition = expresionPosition;
                return param;
            }

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
                    {
                        iVariable.IncrementOrDecrement = new RightIncrement();
                        ConsumeNextToken();
                    }
                     

                    else if (CompareTokenType(TokenTypes.Decrement))
                    {
                        iVariable.IncrementOrDecrement = new LeftIncrement();
                        ConsumeNextToken();
                    }
                       
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
            exp.NodePosition = new Token() {Type = _currentToken.Type,Column = _currentToken.Column, Lexeme = _currentToken.Lexeme, Row = _currentToken.Row};
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
