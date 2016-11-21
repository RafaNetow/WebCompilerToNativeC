﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebCompilerToNativeC.Lexer;
using WebCompilerToNativeC.Tree;
using WebCompilerToNativeC.Tree.DataType;
using WebCompilerToNativeC.Tree.DataType.BaseClass;
using WebCompilerToNativeC.Tree.DataType.Boolean;
using WebCompilerToNativeC.Tree.DataType.LiteralWithIncrOrDecre;

namespace WebCompilerToNativeC.Parser
{
   public class HandlerToSyntactic
   {

        public Dictionary<TokenTypes, LiteralWithOptionalIncrementOrDecrement> LiteralWithDecreOrIncre = new Dictionary<TokenTypes, LiteralWithOptionalIncrementOrDecrement>();
        public Dictionary<TokenTypes, DataType> DataTypes = new Dictionary<TokenTypes, DataType>();


        public HandlerToSyntactic()
       {
           InitLiteralWithDecreOrIncre();
           InitDataTypes();
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
