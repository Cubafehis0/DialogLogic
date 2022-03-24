using MyExpressionParse;
using SemanticTree.Expression;
using System.Xml;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class AddCard2Hand : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        [XmlElement(ElementName = "name")]
        public string CardName { get; set; }

        private IExpression num;
        private Card prefab;
        public AddCard2Hand()
        {
            NumExpression = "";
            CardName = "";
            num = null;
            prefab = null;
        }
        public AddCard2Hand(string name, IExpression num)
        {
            NumExpression = "";
            CardName = "";
            this.num = num;
            this.prefab = StaticCardLibrary.Instance.GetByName(name);
        }
        public AddCard2Hand(XmlNode xmlNode)
        {
            NumExpression = "";
            CardName = "";
            if (!xmlNode.Name.Equals("add_card_to_hand")) throw new SemanticException();
            XmlNode xml = xmlNode.FirstChild;
            while (xml != null)
            {
                switch (xml.Name)
                {
                    case "name":
                        prefab = StaticCardLibrary.Instance.GetByName(xml.InnerText);
                        break;
                    case "num":
                        num = MyExpressionParse.ExpressionParser.AnalayseExpression(xml.InnerText);
                        break;
                }
                xml = xml.NextSibling;
            }
        }
        public override void Execute()
        {
            int b = num.Value;
            for (int i = 0; i < b && !Context.PlayerContext.IsHandFull; i++)
            {
                Card newCard = CardGameManager.Instance.GetCardCopy(prefab);
                Context.PlayerContext.Hand.Add(newCard);
            }
        }
        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
            prefab = StaticCardLibrary.Instance.GetByName(CardName);
        }
    }
}
