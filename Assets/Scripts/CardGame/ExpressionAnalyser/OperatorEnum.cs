using System;

namespace ExpressionAnalyser
{
    [Flags]
    public enum OperatorEnum
    {
        Lp,
        Rp,
        Plus,
        Minus,
        Mul,
        Div,
        Mod,
        Lt,
        Le,
        Equ,
        Ne,
        Ge,
        Gt,
        And,
        Or,
        Not,
    }
}
