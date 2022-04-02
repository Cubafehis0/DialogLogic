namespace ExpressionAnalyser
{
    public interface IVariableAdapter
    {
        bool Contains(string name);
        int this[string name] { get; }
    }

}
