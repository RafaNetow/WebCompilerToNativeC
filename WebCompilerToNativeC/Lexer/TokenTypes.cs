using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCompilerToNativeC.Lexer
{
    public enum TokenTypes
    {
        Sum,
        Mul,
        Sub,
        Eof,    
        LParenthesis,
        Id,
        NumericalLiteral,
        StringLiteral,
        Common,
        RParenthesis,
        Modulus,
        Decrement,
        Increment,
        Asiggnation,
        OpenBracket,
        CloseBracket,
        GreaterThan,
        LessThan,
        IfEqual,
        UnEqual,
        GreaterThanOrEqual,
        LessThanOrEqual,
        And,
        Or,
        Not,
        AndBinary,
        OrBinary,
        XorBinary,
        LeftShift,
        AddAndAssignment,
        SubAndAssignment,
        Div,
        MulAndAssignment,
        DivAndAssignment,
        BitwiseInclusiveOrAndAssignment,
        BitwiseExclusiveOrAndAssignment,
        BitwiseAndAndAssignment,
        ModulAndAssignment,
        ComplementBinary,
        Pointer,
        RwInt,
        RwFloat,
        RwChar,
        RwBool,
        RwString,
        RwDate,
        RwConst,
        RwEnum,
        RwIf,
        RwWhile,
        RwDo,
        RwContinue,
        RwSwitch,
        RwFor,
        RwBreak
    }
}
