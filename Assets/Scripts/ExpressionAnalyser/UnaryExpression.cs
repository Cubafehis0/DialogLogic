namespace ExpressionAnalyser
{
    public class UnaryExpression : IExpression
    {
        private OperatorEnum ope;
        private IExpression operand;
        public UnaryExpression(OperatorEnum ope, IExpression operand)
        {
            this.ope = ope;
            this.operand = operand;
        }
        public int Value
        {
            get => ope switch
            {
                OperatorEnum.Plus => operand.Value,
                OperatorEnum.Minus => -operand.Value,
                OperatorEnum.Not => operand.Value != 0 ? 0 : 1,
                _ => throw new ParseException($"不合理的一元操作符：{ope}")
            };
        }
    }
}
