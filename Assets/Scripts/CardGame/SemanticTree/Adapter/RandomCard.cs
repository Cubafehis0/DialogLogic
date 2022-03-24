using SemanticTree;
using SemanticTree.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.Adapter
{
    /// <summary>
    /// effect
    /// 有缺陷action = null;
    /// </summary>
    public class RandomCard : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        private IExpression num = null;

        [XmlElement(ElementName = "action")]
        public EffectList Actions { get; set; }

        public RandomCard()
        {
            NumExpression = "";
            Actions = new EffectList();
        }

        public RandomCard(string num, Effect action)
        {
            NumExpression = num;
            this.Actions = action;
            Construct();
        }

        public RandomCard(XmlNode xmlNode)
        {
            XmlNode xml = xmlNode.FirstChild;
            while (xml != null)
            {
                switch (xml.Name)
                {
                    case "action":
                        throw new NotImplementedException();
                    //action = SemanticAnalyser.AnalayseEffectList(xml);
                    case "num":
                        NumExpression = xml.InnerText;
                        break;
                }
                xml = xml.NextSibling;
            }
            Construct();
        }

        public override void Execute()
        {
            List<Card> tmp = new List<Card>(Context.PileContext);
            MyMath.Shuffle(tmp);
            foreach (Card card in tmp.GetRange(0, num.Value))
            {
                Context.PushCardContext(card);
                Actions.Execute();
                Context.PopCardContext();
            }
        }

        public override void Construct()
        {
            num = MyExpressionParse.ExpressionParser.AnalayseExpression(NumExpression);
            Actions.Execute();
        }
    }
}
