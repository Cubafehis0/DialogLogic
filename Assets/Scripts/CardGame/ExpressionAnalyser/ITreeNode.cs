using SemanticTree;
using SemanticTree.Expression;

namespace MyExpressionParse
{
    //表达式树的非叶子节点
    public interface IOperator:ITreeNode
    {
        Operator Operator { get; }
        int Proirity { get; }
    }
    public class ConstNode : IExpression
    {
        private readonly int value;
        public ConstNode(int value) => this.value = value;
        public int Value { get => value; }
    }
   
    public class UnaryExpression:IExpression
    {
        private readonly Operator ope;
        private readonly IExpression operand;
        public UnaryExpression(IOperator iope, IExpression operand)
        {
            this.ope = iope.Operator;
            this.operand = operand;
        }
        public int Value
        {
            get => ope switch
            {
                Operator.Plus => operand.Value,
                Operator.Minus => -operand.Value,
                _ => throw new ParseException("非法的一元操作符"),
            };
        }
    }
    public class BinExpression : IExpression
    {
        protected readonly Operator ope;
        private readonly IExpression left, right;
        public BinExpression(IOperator ope, IExpression left, IExpression right)
        {
            this.ope = ope.Operator;
            this.left = left;
            this.right = right;
        }
        public int Value
        {
            get => ope switch
            {
                Operator.Minus => left.Value - right.Value,
                Operator.Plus => left.Value + right.Value,
                Operator.Mul => left.Value * right.Value,
                Operator.Div => left.Value * right.Value,
                _ => throw new ParseException("非法的二元操作符"),
            };
        }
    }
    //特殊的表达式树节点，其值代表了优先级，但是和
    public class OperatorNode : IOperator
    {
        private Operator ope;
        public OperatorNode(Operator ope)
        {
            this.ope = ope;
        }
        public OperatorNode(string ope)
        {
            this.ope = OperatorTool.GetOperator(ope);
        }
        public Operator Operator=> this.ope;
        public int Proirity => OperatorTool.GetOpePriority(this.ope);
    }
}
