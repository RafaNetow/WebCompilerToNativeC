using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Lexer;
using WebCompilerToNativeC.Semantic;
using WebCompilerToNativeC.Semantic.BaseClass;
using WebCompilerToNativeC.Tree;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.DataType.BaseClass;
using WebCompilerToNativeC.Tree.DataType.Boolean;
using WebCompilerToNativeC.Tree.DataType.Char;
using WebCompilerToNativeC.Tree.DataType.IdNode;
using WebCompilerToNativeC.Tree.DataType.IdNode.Accesors;
using WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre;
using WebCompilerToNativeC.Tree.UnaryNode;

namespace WebCompilerToNativeC.Parser
{
   public class HandlerToSyntactic
   {

        public Dictionary<TokenTypes, LiteralWithOptionalIncrementOrDecrement> LiteralWithDecreOrIncre = new Dictionary<TokenTypes, LiteralWithOptionalIncrementOrDecrement>();
        public Dictionary<TokenTypes, DataType> DataTypes = new Dictionary<TokenTypes, DataType>();
        public Dictionary<TokenTypes, UnaryNode> UnariesNode = new Dictionary<TokenTypes, UnaryNode>();
        public Dictionary<TokenTypes, BinaryOperator> OperatorsMul = new Dictionary<TokenTypes, BinaryOperator>();
        public Dictionary<TokenTypes, BinaryOperator> AdditionOp = new Dictionary<TokenTypes, BinaryOperator>(); 
        public Dictionary<TokenTypes, BinaryOperator> RelationalOp = new Dictionary<TokenTypes, BinaryOperator>();
        public Dictionary<TokenTypes, BinaryOperator> AssignationOperator = new Dictionary<TokenTypes, BinaryOperator>();
        public Dictionary<string, DataType> DataTypesLexeme = new Dictionary<string, DataType>();
        public Dictionary<TokenTypes,AccesorNode>TypeOfAccesors = new Dictionary<TokenTypes, AccesorNode>();

        public HandlerToSyntactic()
       {
            InitLiteralWithDecreOrIncre();
            InitDataTypes();
            InitUnariesNode();
            InitOperatorsMul();
            InnitAdditionOp();
            InitRelationalOp();
            IntitAssignationOp();
            InitDataTypesLexeme();
            InitTypeOfAccesors();
       }

       public void InitTypeOfAccesors()
       {
            TypeOfAccesors.Add(TokenTypes.reference, new ReferenceNode());
            TypeOfAccesors.Add(TokenTypes.Point, new PropertyAccesorNode());
           

       }

       private void InitDataTypesLexeme()
       {
            DataTypesLexeme.Add("string", new StringNode());
            DataTypesLexeme.Add("bool", new BooleanNode());
            DataTypesLexeme.Add("char", new CharNode());
            DataTypesLexeme.Add("int", new IntNode());
            DataTypesLexeme.Add("float", new Float());
            DataTypesLexeme.Add("decimal", new DecimalNode());

        }

        private void IntitAssignationOp()
       {
           AssignationOperator.Add(TokenTypes.AddAndAssignment, new AddAndAssignmentNode());
           AssignationOperator.Add(TokenTypes.SubAndAssignment, new SubAndAssignment());
           AssignationOperator.Add(TokenTypes.MulAndAssignment, new MulAndAssignment());
           AssignationOperator.Add(TokenTypes.DivAndAssignment, new DivAndAssignmentNode());
            AssignationOperator.Add(TokenTypes.Asiggnation, new AssignationBinary());

        }

       private void InitRelationalOp()
       {
           RelationalOp.Add(TokenTypes.LessThan, new LessThanOperatorNode());
           RelationalOp.Add(TokenTypes.LessThanOrEqual, new LessOrEqualOperatorNode());
           RelationalOp.Add(TokenTypes.GreaterThan, new GreaterOperatorNode());
           RelationalOp.Add(TokenTypes.GreaterThanOrEqual, new GreaterOrEqualOperatorNode());
           RelationalOp.Add(TokenTypes.IfEqual, new EqualityOperator());
           RelationalOp.Add(TokenTypes.UnEqual, new NotEqualt());
         RelationalOp.Add(TokenTypes.Asiggnation, new AssignationBinary());
        }

       private void InnitAdditionOp()
       {
           AdditionOp.Add(TokenTypes.Sub,new SubNode() );
           AdditionOp.Add(TokenTypes.Sum, new AddNode());
       }

       private void InitOperatorsMul()
       {
           OperatorsMul.Add(TokenTypes.Div, new DivNode());
           OperatorsMul.Add(TokenTypes.Mul, new MultNode());
           OperatorsMul.Add(TokenTypes.Modulus, new ModulusNode());
       }

       private void InitUnariesNode()
       {
           UnariesNode.Add(TokenTypes.Increment, new LeftIncrement());
           UnariesNode.Add(TokenTypes.Decrement, new LeftDecrement());
            UnariesNode.Add(TokenTypes.AndBinary, new AndBinary());
            UnariesNode.Add(TokenTypes.ComplementBinary, new ComplementNode());
            UnariesNode.Add(TokenTypes.OrBinary, new OrUnary());
            UnariesNode.Add(TokenTypes.XorBinary, new XorBinary() );
            UnariesNode.Add(TokenTypes.Sub, new NegativeNode());
            UnariesNode.Add(TokenTypes.Mul, new MulUnary());
            UnariesNode.Add(TokenTypes.Not, new NotUnary());
          
        }

        private void InitDataTypes()
       {
            DataTypes.Add(TokenTypes.StringLiteral, new StringNode());
            DataTypes.Add(TokenTypes.True, new BooleanNode());
            DataTypes.Add(TokenTypes.DateLiteral, new DateNode());
            DataTypes.Add(TokenTypes.False, new BooleanNode());
            DataTypes.Add(TokenTypes.CharLiteral, new CharNode() );
            DataTypes.Add(TokenTypes.NumericalLiteral, new IntNode());
            DataTypes.Add(TokenTypes.HexadecimalLiteral, new HexaNode());
            DataTypes.Add(TokenTypes.FloatLiteral, new Float());         
            DataTypes.Add(TokenTypes.DecimalLiteral, new DecimalNode());




        }

       private void InitLiteralWithDecreOrIncre()
       {
            LiteralWithDecreOrIncre.Add(TokenTypes.NumericalLiteral, new IntNode());
            LiteralWithDecreOrIncre.Add(TokenTypes.HexadecimalLiteral, new HexaNode());
            LiteralWithDecreOrIncre.Add(TokenTypes.FloatLiteral, new Float() );
            LiteralWithDecreOrIncre.Add(TokenTypes.DecimalLiteral,new DecimalNode() );
        

        }

    

       public Result CheckToken(TokenTypes expectToken, Token currentToken)
       {

           if (expectToken == currentToken.Type)
               return new Result() {Succes = true, Excpetion = null};

           return new Result {Succes = false,Excpetion = new SyntacticException($"Expected a {expectToken.ToString()} Token ",currentToken.Row,currentToken.Column)};


       }

        //When dnt found any production that could be allowed
       public Exception DefaultError(Token currentToken)
       {
            return new  SyntacticException("UnExpected Token"+currentToken.Type.ToString() +" ",currentToken.Row,currentToken.Column);
    }
    }

    internal class MulUnary : UnaryNode
    {
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }

    internal class XorBinary : UnaryNode
    {
        public override BaseType ValidateSemantic()
        {
            throw new NotImplementedException();
        }

        public override string GenerateCode()
        {
            throw new NotImplementedException();
        }
    }

    public class Result
    {
        public bool Succes { get; set; }
        public SyntacticException Excpetion { get; set; }
    }
}
