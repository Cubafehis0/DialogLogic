using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class StringCondition : ConditionNode
    {
        [XmlElement]
        public string ConditionExpression;

        private IExpression expression;
        public override bool Value
        {
            get
            {
                return expression.Value != 0;
            }   
        }

        public override void Construct()
        {
            expression = ExpressionParser.AnalayseExpression(ConditionExpression);
        }
    }
}
