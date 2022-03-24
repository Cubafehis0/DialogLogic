﻿using SemanticTree.Expression;
using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class AddStatus : Effect
    {

        [XmlElement(ElementName = "name")]
        public string StatusName { get; set; }

        [XmlElement(ElementName = "value")]
        public string ValueExpression { get; set; }

        private IExpression value;
        private Status status = null;

        public AddStatus()
        {
            StatusName = "";
            ValueExpression = "";
            value = null;
            status = null;
        }

        public AddStatus(Status status, int value)
        {
            this.status = status;
            this.value = new ConstInt(value);
        }

        public AddStatus(XmlNode xmlNode)
        {
            if (!xmlNode.Name.Equals("add_status")) throw new SemanticException();
            if (!xmlNode.HasChildNodes) throw new SemanticException();
            XmlNode xml = xmlNode.FirstChild;
            while (xml != null)
            {
                switch (xml.Name)
                {
                    case "name":
                        status = StaticStatusLibrary.GetByName(xml.InnerText);
                        break;
                    case "value":
                        value = MyExpressionParse.ExpressionParser.AnalayseExpression(xml.InnerText);
                        break;
                }
                xml = xml.NextSibling;
            }
        }

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddStatusCounter(Context.PlayerContext, status, value.Value);
        }

        public override void Construct()
        {
            status = StaticStatusLibrary.GetByName(StatusName);
            value = MyExpressionParse.ExpressionParser.AnalayseExpression(ValueExpression);
        }
    }
}
