namespace ExpressionAnalyser
{
    public class BinocularExpression : IExpression
    {
        protected OperatorEnum ope;
        private IExpression left, right;
        public BinocularExpression(OperatorEnum ope, IExpression left, IExpression right)
        {
            this.ope = ope;
            this.left = left;
            this.right = right;
        }
        public int Value
        {
            get => ope switch
            {
                OperatorEnum.Minus => left.Value - right.Value,
                OperatorEnum.Plus => left.Value + right.Value,
                OperatorEnum.Mul => left.Value * right.Value,
                OperatorEnum.Div => left.Value / right.Value,
                OperatorEnum.Mod => left.Value % right.Value,

                OperatorEnum.Le => left.Value <= right.Value ? 1 : 0,
                OperatorEnum.Lt => left.Value < right.Value ? 1 : 0,
                OperatorEnum.Ne => left.Value != right.Value ? 1 : 0,
                OperatorEnum.Equ => left.Value == right.Value ? 1 : 0,
                OperatorEnum.Ge => left.Value >= right.Value ? 1 : 0,
                OperatorEnum.Gt => left.Value > right.Value ? 1 : 0,

                OperatorEnum.And => left.Value != 0 && right.Value != 0 ? 1 : 0,
                OperatorEnum.Or => left.Value != 0 || right.Value != 0 ? 1 : 0,
                _ => throw new ParseException($"不合理的二元操作符：{ope}")
            };
        }
    }
}
