using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpressionAnalyser
{
    #region 词法规则
    /*词法分析
     * [a-zA-Z_][a-zA-Z0-9_]*   id
     *  +                       Plus
     *  -                       Minus
     *  *                       Mul
     *  /                       Div
     *  %                       Mod
     *  &&                      And
     *  ||                      Or
     *  !                       Not
     *  (                       Lp
     *  )                       Rp
     * ([+-])[0-9]+             Interger
     */
    #endregion
    //需要转义的字符：$, (, ), *, +, ., [, ], ?, \, ^, {, }, |

    public enum LexType
    {
        Integer,
        Identify,
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

    public static class LexAnalysis
    {
        readonly static List<KeyValuePair<LexType, string>> LexRegular = new List<KeyValuePair<LexType, string>>()
        {
            new KeyValuePair<LexType, string>(LexType.Identify,@"[a-zA-Z_][a-zA-Z0-9_\.]*"),

            new KeyValuePair<LexType, string>(LexType.Plus,@"\+"),
            new KeyValuePair<LexType, string>(LexType.Minus,@"-" ),
            new KeyValuePair<LexType, string>(LexType.Mul,@"\*" ),
            new KeyValuePair<LexType, string>(LexType.Div,@"/"),
            new KeyValuePair<LexType, string>(LexType.Mod,@"%"),

            new KeyValuePair<LexType, string>(LexType.Le,@"<="),
            new KeyValuePair<LexType, string>(LexType.Lt,@"<"),
            new KeyValuePair<LexType, string>(LexType.Ge,@">="),
            new KeyValuePair<LexType, string>(LexType.Gt,@">"),
            new KeyValuePair<LexType, string>(LexType.Equ,@"=="),
            new KeyValuePair<LexType, string>(LexType.Ne,@"!="),

            new KeyValuePair<LexType, string>(LexType.And,@"&&"),
            new KeyValuePair<LexType, string>(LexType.Or,@"\|\|"),
            new KeyValuePair<LexType, string>(LexType.Not,@"!"),

            new KeyValuePair<LexType, string>(LexType.Lp,@"\("),
            new KeyValuePair<LexType, string>(LexType.Rp,@"\)"),
            new KeyValuePair<LexType, string>(LexType.Integer,@"[+-]?[0-9]+"),
        };
        public static List<Token> Lex(string s)
        {
            List<Token> result = new List<Token>();
            foreach (var p in LexRegular)
            {
                s = AnalysisLexParttern(s, p, result);
            }
            IsValid(s);
            return result;
        }
        private static void IsValid(string s)
        {
            //Debug.Log(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsWhiteSpace(s[i]))
                {
                    throw new ParseException("字符串含有不支持的词法规则", s, i);
                }
            }
        }
        private static string AnalysisLexParttern(string s,KeyValuePair<LexType, string> parttern, List<Token> tokens)
        {
            var matches = Regex.Matches(s, parttern.Value);
            foreach (Match match in matches)
            {
                string v = match.Groups[0].Value;
                Token t = new Token(parttern.Key, v, s.IndexOf(v));
                tokens.Add(t);
                int index = s.IndexOf(v);
                s = s.Substring(0, index) + new String(' ', v.Length) + s.Substring(index + v.Length);
            }
            return s;
        }
    }
}
