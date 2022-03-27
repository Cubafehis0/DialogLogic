using UnityEngine;

namespace ExpressionAnalyser
{
    /// <summary>
    /// 需要更改的地方，传入值为节点名字（变量名字）
    /// </summary>
    public class VariableNode : IExpression
    {
        private readonly string name;
        public VariableNode(string name) => this.name = name;
        public int Value
        {
            get
            {
                if (GlobalExpression.varibles.TryGetValue(name, out var value))
                    return value.Value;
                else
                {
                    Debug.LogError($"全局变量表中不存在{name}变量");
                    return 0;
                }
            }

        }
    }
}
