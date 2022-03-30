using System.Collections.Generic;

namespace ExpressionAnalyser
{
    public enum OperatorFlag : uint
    {
        None,
        Left,
        Right,
    }

    public static class OperatorTool
    {
        readonly static Dictionary<OperatorEnum, string> ope2Str = new Dictionary<OperatorEnum, string>()
        {
            {OperatorEnum.Plus,"+" },
            {OperatorEnum.Minus,"-"},
            {OperatorEnum.Mul,"*"},
            {OperatorEnum.Div,"/"},
            {OperatorEnum.Mod,"%"},

            {OperatorEnum.Lt,"<"},
            {OperatorEnum.Le,"<=" },
            {OperatorEnum.Equ,"==" },
            {OperatorEnum.Ne,"!=" },
            {OperatorEnum.Gt,">"},
            {OperatorEnum.Ge,">=" },

            {OperatorEnum.And,"&&" },
            {OperatorEnum.Or,"||" },
            {OperatorEnum.Not,"!" },

            {OperatorEnum.Lp,"(" },
            {OperatorEnum.Rp,")"},
        }; 
        readonly static Dictionary<string,OperatorEnum> str2Ope = new Dictionary< string, OperatorEnum>()
        {
            {"+" ,OperatorEnum.Plus},
            {"-",OperatorEnum.Minus},
            {"*", OperatorEnum.Mul},
            {"/" ,OperatorEnum.Div},
            {"%" ,OperatorEnum.Mod},

            {"<" ,OperatorEnum.Lt},
            {"<=" ,OperatorEnum.Le},
            {"==" ,OperatorEnum.Equ},
            {"!=" ,OperatorEnum.Ne},
            {">" ,OperatorEnum.Gt},
            {">=" ,OperatorEnum.Ge},

            {"&&" ,OperatorEnum.And},
            {"||" ,OperatorEnum.Or},
            {"!" ,OperatorEnum.Not},

            {"(" ,OperatorEnum.Lp},
            {")" ,OperatorEnum.Rp},
        };
       
        readonly static List<OperatorEnum> Operators = new List<OperatorEnum>()
        {
            OperatorEnum.Plus,OperatorEnum.Minus,OperatorEnum.Mul,OperatorEnum.Div,OperatorEnum.Mod,
            OperatorEnum.And,OperatorEnum.Or,OperatorEnum.Not,
            OperatorEnum.Lp,OperatorEnum.Rp,
        };
        readonly static List<OperatorEnum> LeftOperator = new List<OperatorEnum>()
        {
            OperatorEnum.Plus,OperatorEnum.Minus,OperatorEnum.Mul,OperatorEnum.Div,OperatorEnum.Mod,
            OperatorEnum.Le,OperatorEnum.Lt,OperatorEnum.Gt,OperatorEnum.Ge,OperatorEnum.Equ,OperatorEnum.Ne,
            OperatorEnum.And,OperatorEnum.Or,
        };
        readonly static List<OperatorEnum> RightOperator = new List<OperatorEnum>()
        {
            OperatorEnum.Plus,OperatorEnum.Minus,
            OperatorEnum.Not,
        };
        readonly static Dictionary<OperatorEnum, int> LeftOpePriority = new Dictionary<OperatorEnum, int>()
        {
            {OperatorEnum.Lp,2 },
            {OperatorEnum.Mul,5},
            {OperatorEnum.Div,5},
            {OperatorEnum.Mod,5},
            {OperatorEnum.Plus,6},
            {OperatorEnum.Minus,6},
            {OperatorEnum.Le,8},
            {OperatorEnum.Lt,8 },
            {OperatorEnum.Gt,8},
            {OperatorEnum.Ge,8},
            {OperatorEnum.Equ,9},
            {OperatorEnum.Ne,9},
            {OperatorEnum.And,13 },
            {OperatorEnum.Or,14},
            {OperatorEnum.Rp,15 },
        };

        readonly static Dictionary<OperatorEnum, int> RightOpePriority = new Dictionary<OperatorEnum, int>()
        {
            { OperatorEnum.Minus,3},
            {OperatorEnum.Plus,3 },
            {OperatorEnum.Not,3 },
        };
        public static bool IsLeftOpe(OperatorEnum ope)
        {
            return LeftOperator.Contains(ope);
        }
        public static bool IsRightOpe(OperatorEnum ope)
        {
            return RightOperator.Contains(ope);
        }

        public static int GetRightOpePriority(OperatorEnum ope)
        {
            return RightOpePriority[ope];
        }
        public static int GetOpePriority(OperatorEnum ope)
        {
            return LeftOpePriority[ope];
        }
        public static int GetOpePriority(string s)
        {
            return GetOpePriority(GetOperator(s));
        }
        public static OperatorEnum GetOperator(string s)
        {
            return str2Ope[s];
        }
        public static string GetOperator(OperatorEnum ope)
        {
            return ope2Str[ope];
        }
    }
}
