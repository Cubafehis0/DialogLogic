namespace ExpressionAnalyser
{
    public class ConstNode : IExpression
    {
        private readonly int value = 0;
        public ConstNode(int value)
        {
            this.value = value;
        }
        public int Value { get => value; }
    }
}
