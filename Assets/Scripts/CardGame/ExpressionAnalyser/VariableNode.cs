namespace ExpressionAnalyser
{
    /// <summary>
    /// 需要更改的地方，传入值为节点名字（变量名字）
    /// </summary>
    public class VariableNode : IExpression
    {
        private readonly string name;
        public VariableNode(string name) => this.name = name;
        public int Value { get => name.Length; }
    }
}
