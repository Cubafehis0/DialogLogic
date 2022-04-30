using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    public class AddStrength : Effect
    {
        [XmlElement(ElementName = "value")]
        public string NumExpression;
        private IExpression numExpression;

        public override void Construct()
        {
            numExpression = ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            Context.PlayerContext.Strength += numExpression.Value;
        }
    }
}
