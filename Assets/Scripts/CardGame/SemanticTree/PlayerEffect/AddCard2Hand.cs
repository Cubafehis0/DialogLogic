using ExpressionAnalyser;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    [XmlType(TypeName ="add_card_to_hand")]
    [Serializable]
    public class AddCard2Hand : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression;
        [XmlElement(ElementName = "name")]
        public string CardName;

        private IExpression num;
        private Card prefab;
        public AddCard2Hand()
        {
            NumExpression = "";
            CardName = "";
            num = null;
            prefab = null;
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
                        num = ExpressionAnalyser.ExpressionParser.AnalayseExpression(xml.InnerText);
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
