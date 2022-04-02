using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    public class GUISelectNerfCondition:Effect
    {
        [XmlElement(ElementName = "value")]
        public string ValueExpression;
        private IExpression expression;


        public override void Construct()
        {
            expression = ExpressionParser.AnalayseExpression(ValueExpression);
        }

        public override void Execute()
        {
            CardGameManager.Instance.OpenConditionNerfPanel(expression.Value);
        }
    }
}
