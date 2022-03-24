using System;
using SemanticTree;
using SemanticTree.Expression;

namespace MyExpressionParse
{
    [Flags]
    enum TokenType
    {
        Operand,
        Operator
    }
    class Token
    {
        public TokenType type;
        public ITreeNode node;
        public int begIndex;
        public Token(TokenType type, ITreeNode node, int begIndex)
        {
            this.type = type;
            this.node = node;
            this.begIndex = begIndex;
        }
        public Token(LexType type, string value, int begIndex)
        {
            switch (type)
            {
                case LexType.Identify:
                    node = new GetGlobalVariable(value);
                    this.type = TokenType.Operand;
                    break;
                case LexType.Integer:
                    int.TryParse(value, out int tmp);
                    node = new ConstNode(tmp);
                    break;
                case LexType.Lp:
                case LexType.Rp:
                case LexType.Plus:
                case LexType.Minus:
                case LexType.Mul:
                case LexType.Div:
                    node = new OperatorNode(value);
                    this.type= TokenType.Operator;
                    break;
            }
            this.begIndex = begIndex;
        }
    }
}
