﻿using SemanticTree.Expression;
using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// Expression参数num
    /// </summary>
    public class ModifyHealthNode : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }

        private IExpression num;

        public ModifyHealthNode()
        {
            NumExpression = "";
            this.num = null;
        }

        public ModifyHealthNode(XmlNode xmlNode)
        {

            num = MyExpressionParse.ExpressionParser.AnalayseExpression(xmlNode.InnerText);
        }

        public override void Construct()
        {
            num = MyExpressionParse.ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            Context.PlayerContext.Player.Health += num.Value;
        }
    }
}
