using System;

namespace ExpressionAnalyser
{
    [Flags]
    public enum TokenType
    {
        Operand,
        Operator
    }
    public class Token
    {
        public TokenType type;
        private object value;
        public int begIndex;
        public Token(TokenType type, object value, int begIndex)
        {
            this.type = type;
            if (type == TokenType.Operand)
                if (!(value is IExpression))
                    throw new ParseException("无法为一个操作树节点存储一个非IExpression值");
            if (type == TokenType.Operator)
                if (!(value is OperatorEnum))
                    throw new ParseException("无法为一个操作符节点存储一个非Operator值");
            this.value = value;
            this.begIndex = begIndex;
        }
        public Token(LexType type, string value, int begIndex)
        {
            switch (type)
            {
                case LexType.Identify:
                    this.value = new VariableNode(value);
                    this.type = TokenType.Operand;
                    break;
                case LexType.Integer:
                    int.TryParse(value, out int tmp);
                    this.value = new ConstNode(tmp);
                    this.type = TokenType.Operand;
                    break;
                default:
                    this.value = OperatorTool.GetOperator(value);
                    this.type = TokenType.Operator;
                    break;
            }
            this.begIndex = begIndex;
        }
        public OperatorEnum Ope
        {
            get
            {
                if (type == TokenType.Operator)
                {
                    return (OperatorEnum)value;
                }
                else throw new ParseException("无法获取一个非操作符节点的操作符值");
            }
        }
        public IExpression Expression
        {
            get
            {
                if (type == TokenType.Operand)
                {
                    return value as IExpression;
                }
                else throw new ParseException("无法获取一个非操作数节点的操作数值");
            }
        }
    }
}
