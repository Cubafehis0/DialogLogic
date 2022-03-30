using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpressionAnalyser
{

    /*语法分析
     *  exp  exp binOperator exp
     *       lp exp rp
     *       unaryOperator exp
     */
    public static class ExpressionParser
    {
        class OperatorFlagWrap
        {
            public OperatorEnum ope;
            public OperatorFlag flag;
            public int Priority
            {
                get => flag switch
                {
                    OperatorFlag.Right => OperatorTool.GetRightOpePriority(ope),
                    _ => OperatorTool.GetOpePriority(ope),
                };
            }
            public OperatorFlagWrap(OperatorEnum ope, OperatorFlag flag)
            {
                this.ope = ope;
                this.flag = flag;
            }
            public void Refresh(OperatorFlagWrap warp)
            {
                this.ope = warp.ope;
                this.flag = warp.flag;
            }
            public OperatorFlagWrap()
            {
                flag = OperatorFlag.None;
            }
        }

        public static IVariableAdapter VariableTable { get; set; }

        public static IExpression AnalayseExpression(string s)
        {
            List<Token> tokens = LexAnalysis.Lex(s);
            tokens.Sort((l, r) => l.begIndex - r.begIndex);
            return Parse(tokens);
        }

        private static IExpression Parse(List<Token> tokens)
        {
            Stack<OperatorFlagWrap> opeStack = new Stack<OperatorFlagWrap>();
            Stack<Token> parseStack = new Stack<Token>();
            OperatorFlagWrap currentOpe;
            for (int i = 0; i < tokens.Count; i++)
            {
                Token token = tokens[i];
                if (token.type == TokenType.Operand)
                {
                    parseStack.Push(token);
                    continue;
                }
                OperatorEnum ope = token.Ope;
                currentOpe = GetCurOpe(parseStack, ope);
                ReductionExpression(opeStack, parseStack, currentOpe);
                if (currentOpe.ope == OperatorEnum.Rp)
                {
                    TryRedutionParenth(parseStack, opeStack);
                    ReductionExpression(opeStack, parseStack, currentOpe);
                }
                else
                {
                    parseStack.Push(token);
                    opeStack.Push(currentOpe);
                }
            }
            currentOpe = null;
            ReductionExpression(opeStack, parseStack, currentOpe);
            if (parseStack.Count == 1)
            {
                return parseStack.Pop().Expression;
            }
            else throw new ParseException("操作数和操作符不匹配");
        }
        private static void ReductionExpression(Stack<OperatorFlagWrap> opeStack, Stack<Token> parseStack, OperatorFlagWrap currentOpe)
        {
            bool success = true;
            while (success)
            {
                success = success && (ReductionRightExpression(parseStack, opeStack) ||
                                     ReductionLeftExpression(parseStack, opeStack, currentOpe));
            }
        }

        private static void TryRedutionParenth(Stack<Token> parseStack, Stack<OperatorFlagWrap> opeStack)
        {
            try
            {
                ReductionParenth(parseStack, opeStack);
            }
            catch (Exception)
            {
                throw new ParseException("括号的数量不匹配");
            }
        }
        private static void ReductionParenth(Stack<Token> parseStack, Stack<OperatorFlagWrap> opeStack)
        {
            Token token = parseStack.Pop();
            parseStack.Pop();
            opeStack.Pop();
            parseStack.Push(token);
        }
        private static bool ReductionLeftExpression(Stack<Token> parseStack, Stack<OperatorFlagWrap> opeStack, OperatorFlagWrap currentOpe)
        {
            if (parseStack.Count == 0) return false;
            if (opeStack.Count == 0) return false;
            var lastOpe = opeStack.Peek();
            if (lastOpe.flag == OperatorFlag.Left)
            {
                if (currentOpe == null || lastOpe.Priority <= currentOpe.Priority)
                {
                    if (parseStack.Count < 3)
                    {
                        throw new ParseException("不匹配的操作数");
                    }
                    var right = parseStack.Pop().Expression;
                    opeStack.Pop();
                    OperatorEnum ope = parseStack.Pop().Ope;
                    var leftToken = parseStack.Pop();
                    parseStack.Push(new Token(TokenType.Operand,
                                    new BinocularExpression(ope, leftToken.Expression, right), leftToken.begIndex));
                    return true;
                }
            }
            return false;
        }

        private static OperatorFlagWrap GetCurOpe(Stack<Token> stack, OperatorEnum ope)
        {
            OperatorFlagWrap res = new OperatorFlagWrap();
            res.ope = ope;
            if (OperatorTool.IsRightOpe(ope))
            {
                if (stack.Count == 0 || stack.Peek().type == TokenType.Operator)
                {
                    res.flag = OperatorFlag.Right;
                    return res;
                }
            }
            if (OperatorTool.IsLeftOpe(ope))
                res.flag = OperatorFlag.Left;
            else res.flag = OperatorFlag.None;
            return res;
        }

        private static bool ReductionRightExpression(Stack<Token> parseStack, Stack<OperatorFlagWrap> opeStack)
        {
            if (parseStack.Count == 0) return false;
            if (opeStack.Count == 0) return false;
            var lastOpe = opeStack.Peek();
            if (lastOpe.flag == OperatorFlag.Right)
            {
                var token = parseStack.Peek();
                if (token.type == TokenType.Operand)
                {
                    parseStack.Pop();
                    parseStack.Pop();
                    opeStack.Pop();
                    parseStack.Push(new Token(TokenType.Operand, new UnaryExpression(lastOpe.ope, token.Expression), token.begIndex));
                    return true;
                }
            }
            return false;
        }
    }
}
