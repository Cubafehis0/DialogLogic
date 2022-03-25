using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.Adapter
{
    /// <summary>
    /// effect
    /// 有缺陷
    /// </summary>
    public class ForEachInPile : Effect
    {
        [XmlElement(ElementName = "condition")]
        public ICondition condition = null;
        [XmlElement(ElementName = "action")]
        public EffectList action = null;

        public ForEachInPile()
        {

        }

        public ForEachInPile(ICondition condition, EffectList action)
        {
            this.condition = condition;
            this.action = action;
        }

        public override void Execute()
        {
            foreach (Card card in Context.PileContext)
            {
                Context.PushCardContext(card);
                if (condition?.Value ?? true) action?.Execute();
                Context.PopCardContext();
            }
        }

        public override void Construct()
        {
            throw new NotImplementedException();
        }

        public ForEachInPile(XmlNode xmlNode)
        {
            XmlNode xml = xmlNode.FirstChild;
            while (xml != null)
            {
                switch (xml.Name)
                {
                    case "action":
                        action = SemanticAnalyser.AnalayseEffectList(xml);
                        break;
                    case "conditon":
                        condition = SemanticAnalyser.AnalyseConditionList(xml);
                        break;
                }
                xml = xml.NextSibling;
            }
        }
    }
}
