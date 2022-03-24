using SemanticTree.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.GlobalEffect
{
    public class SetVariableNode : Effect
    {
        [XmlElement(ElementName ="name")]
        public string Name { get; set; }

        [XmlElement(ElementName ="value")]
        public string Expression { get; set; }

        private IExpression exp;

        public SetVariableNode()
        {
            Name = "";
            Expression = "";
        }

        public SetVariableNode(string name, string exp)
        {
            this.Name = name;
            this.Expression = exp;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
            //if (variableDictionary.ContainsKey(name))
            //{
            //    variableDictionary[name] = exp.Value;
            //}
            //else
            //{
            //    variableDictionary.Add(name, exp.Value);
            //}
        }

        public override void Construct()
        {
            exp = MyExpressionParse.ExpressionParser.AnalayseExpression(Expression);
        }
    }
}
