using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Lexer;
using WebCompilerToNativeC.Tree;
using WebCompilerToNativeC.Tree.BaseClass;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.DataType.BaseClass;
using WebCompilerToNativeC.Tree.DataType.Boolean;
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

        public HandlerToSyntactic()
       {
            InitLiteralWithDecreOrIncre();
            InitDataTypes();
            InitUnariesNode();
            InitOperatorsMul();
            InnitAdditionOp();
            InitRelationalOp();
       }

       private void InitRelationalOp()
       {
           RelationalOp.Add(TokenTypes.LessThan, new LessThanOperatorNode());
           RelationalOp.Add(TokenTypes.LessThanOrEqual, new LessOrEqualOperatorNode());
           RelationalOp.Add(TokenTypes.GreaterThan, new GreaterOperatorNode());
           RelationalOp.Add(TokenTypes.GreaterThanOrEqual, new GreaterOrEqualOperatorNode());
           RelationalOp.Add(TokenTypes.AddAndAssignment, AndNode);
            
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
            UnariesNode.Add(TokenTypes.XorBinary, new NegativeNode());
            UnariesNode.Add(TokenTypes.Mul, new MulUnary());
            UnariesNode.Add(TokenTypes.Not, new NotUnary());
            /*
              if (CompareTokenType(TokenTypes.Increment) || CompareTokenType(TokenTypes.Decrement) ||
                 CompareTokenType(TokenTypes.AndBinary) || CompareTokenType(TokenTypes.ComplementBinary) ||
                 CompareTokenType(TokenTypes.OrBinary) || CompareTokenType(TokenTypes.XorBinary) ||
                 CompareTokenType(TokenTypes.Not) || CompareTokenType(TokenTypes.Sub) || CompareTokenType(TokenTypes.Increment) || CompareTokenType(TokenTypes.Mul)
                  || CompareTokenType(TokenTypes.Decrement))

             {
                 ConsumeNextToken();
             }

              */
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

    public class Result
    {
        public bool Succes { get; set; }
        public SyntacticException Excpetion { get; set; }
    }
}
