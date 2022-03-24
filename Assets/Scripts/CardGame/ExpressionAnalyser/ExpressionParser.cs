using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using SemanticTree;
using SemanticTree.Expression;

namespace MyExpressionParse
{
    public enum LexType
    {
        Integer,
        Identify,
        Lp,
        Rp,
        Plus,
        Minus,
        Mul,
        Div
    }

    /*词法分析
     * ([+-])[0-9]+    int
     *  * mul
     *  + add
     *  - minus
     *  / div
     *  ( lp
     *  ) rp
     *  [a-zA-Z_][a-zA-Z0-9_]* id
     */
    /*语法分析
     *  exp  exp mul exp
     *       minus exp
     *       add exp
     *       exp div exp
     *       exp add exp
     *       lp exp rp
     *       id
     *       int
     */
    public static class ExpressionParser
    {
        readonly static Dictionary<LexType, string> lexParttern = new Dictionary<LexType, string>()
        {
            {LexType.Integer,@"[\+-]?[0-9]+"},
            {LexType.Identify,@"[a-zA-Z_][a-zA-Z0-9_]*"},
            {LexType.Lp,@"\(" },
            {LexType.Rp,@"\)" },
            {LexType.Plus,@"\+" },
            {LexType.Minus,@"-" },
            {LexType.Mul,@"\*" },
            {LexType.Div,@"/" },
        };
        public static IExpression AnalayseExpression(string s)
        {
            List<Token> tokens = new List<Token>();
            s = LexAnalysis(s, tokens);
            IsValid(s);
            Debug.Log("对传入的字符串进行词法分析成功");
            tokens.Sort((l, r) => l.begIndex - r.begIndex); 
            return Parse(tokens);
        }
        private static void IsValid(string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                if(!char.IsWhiteSpace(s[i]))
                {
                    throw new ParseException("字符串含有不支持此法规则",s,i);
                }
            }
        }
        private static IExpression Parse(List<Token> tokens)
        {
            if (tokens.Count == 0) return null;
            ParseUnaryOperator(tokens);
            List<Token> suffix = TransInfix2Suffix(tokens);
            return ParseSuffix(suffix);
        }
        private static void ParseUnaryOperator(List<Token> tokens)
        {
            bool lastIsOperand=false;
            for (int i=0;i<tokens.Count-1;i++)
            {
                Token t = tokens[i];
                if(t.type==TokenType.Operand)
                {
                    lastIsOperand=true;
                }
                else
                {
                    if (!lastIsOperand)
                    {
                        if (tokens[i + 1].type == TokenType.Operand)
                        {
                            Operator ope = (t.node as IOperator).Operator;
                            if (ope == Operator.Minus || ope == Operator.Plus)
                            {
                                tokens.RemoveAt(i);
                                IExpression e = tokens[i].node as IExpression;
                                tokens.RemoveAt(i);
                                UnaryExpression unary = new UnaryExpression(t.node as IOperator, e);
                                tokens.Insert(i, new Token(TokenType.Operand, unary, t.begIndex));
                                lastIsOperand = true;
                                continue;
                            }
                        }
                    }
                    lastIsOperand = false;
                }
            }
        }
        private static IExpression ParseSuffix(List<Token> suffix)
        {
            Stack<IExpression> stack = new Stack<IExpression>();
            foreach (var token in suffix)
            {
                TokenType type = token.type;
                switch (type)
                {
                    case TokenType.Operand:
                        stack.Push(token.node as IExpression);
                        break;
                    case TokenType.Operator:
                        Operator ope = (token.node as IOperator).Operator;
                        if (stack.Count <2)
                        {
                            throw new ParseException($"多余的操作符{OperatorTool.GetOperator(ope)}",token.begIndex);
                        }
                        else
                        {
                            IExpression r = stack.Pop();
                            IExpression l = stack.Pop();
                            stack.Push(new BinExpression(token.node as IOperator, l, r));
                        }
                        break;
                }
            }
            if (stack.Count == 0)
            {
                throw new ParseException("表达式为空。没有实际的值");
            }
            else if (stack.Count > 1)
            {
                throw new ParseException("过多的操作数。操作数和操作符不匹配");
            }
            return stack.Peek();
        }
        private static List<Token> TransInfix2Suffix(List<Token> infix)
        {
            Stack<Token> stack = new Stack<Token>();
            List<Token> suffix = new List<Token>();
            foreach (var token in infix)
            {
                switch (token.type)
                {
                    case TokenType.Operand:
                        suffix.Add(token);
                        break;
                    case TokenType.Operator:
                        Operator ope = (token.node as IOperator).Operator;
                        if (ope == Operator.RP)
                        {
                            while (stack.Count > 0 && (stack.Peek().node as IOperator).Operator != Operator.LP)
                            {
                                suffix.Add(stack.Pop());
                            }
                            stack.Pop();
                        }
                        else
                        {
                            while (stack.Count > 0 &&
                                    (stack.Peek().node as IOperator).Operator != Operator.LP &&
                                    (token.node as OperatorNode).Proirity <= (stack.Peek().node as OperatorNode).Proirity)
                            {
                                suffix.Add(stack.Pop());
                            }
                            stack.Push(token);
                        }
                        break;
                }
            }
            while (stack.Count > 0)
            {
                suffix.Add(stack.Pop());
            }
            return suffix;
        }
        private static string LexAnalysis(string s, List<Token> tokens)
        {
            s = AnalysisLexParttern(s, LexType.Identify, tokens);
            s = AnalysisLexParttern(s, LexType.Lp, tokens);
            s = AnalysisLexParttern(s, LexType.Rp, tokens);
            s = AnalysisLexParttern(s, LexType.Plus, tokens);
            s = AnalysisLexParttern(s, LexType.Minus, tokens);
            s = AnalysisLexParttern(s, LexType.Mul, tokens);
            s = AnalysisLexParttern(s, LexType.Div, tokens);
            s = AnalysisLexParttern(s, LexType.Integer, tokens);
            return s;
        }
        private static string AnalysisLexParttern(string s, LexType type, List<Token> tokens)
        {
            var matches = Regex.Matches(s, lexParttern[type]);
            foreach (Match match in matches)
            {
                string v = match.Groups[0].Value;
                Token t = new Token(type, v, s.IndexOf(v));
                tokens.Add(t);
                int index=s.IndexOf(v);
                s = s.Substring(0, index) + new String(' ', v.Length) + s.Substring(index + v.Length);
            }
            return s;
        }

    }
}
