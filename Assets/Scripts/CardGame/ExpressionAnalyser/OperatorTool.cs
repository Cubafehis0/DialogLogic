using System;
using System.Collections.Generic;

namespace MyExpressionParse
{
    [Flags]
    public enum Operator
    {
        Minus,
        Plus,
        Mul,
        Div,
        LP,
        RP
    }
    public static class OperatorTool
    {
        readonly static Dictionary<Operator, int> opePriority = new Dictionary<Operator, int>()
        {
            {Operator.Minus,0},
            {Operator.Plus,0},
            {Operator.Mul, 1},
            {Operator.Div,1},
            {Operator.LP,2},
            {Operator.RP,2},
        };
        public static int GetOpePriority(Operator ope)
        {
            return opePriority[ope];
        }
        public static int GetOpePriority(string s)
        {
            return GetOpePriority(GetOperator(s));
        }
        public static Operator GetOperator(string s)
        {
            return s switch
            {
                "+" => Operator.Plus,
                "-" => Operator.Minus,
                "*" => Operator.Mul,
                "/" => Operator.Div,
                "(" => Operator.LP,
                ")" => Operator.RP,
                _ => Operator.Plus,
            };
        }
        public static string GetOperator(Operator ope)
        {
            return ope switch
            {
                Operator.Plus => "+",
                Operator.Minus => "-",
                Operator.Mul => "*",
                Operator.Div => "/",
                Operator.LP => "(",
                Operator.RP => ")",
                _ => "",
            };
        }
    }
}
