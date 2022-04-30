using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.GlobalEffect
{
    public class SetGlobalVar : Effect
    {
        [XmlElement(ElementName = "name")]
        public string Name;

        [XmlElement(ElementName = "value")]
        public string Expression;
        private IExpression exp;

        public override void Execute()
        {
            if (Context.variableTable.ContainsKey(Name))
            {
                Context.variableTable[Name] = exp.Value;
            }
            else
            {
                throw new SemanticException();
            }
        }

        public override void Construct()
        {
            Context.variableTable.Add(Name, 0);
            exp = ExpressionParser.AnalayseExpression(Expression);
        }
    }
}
