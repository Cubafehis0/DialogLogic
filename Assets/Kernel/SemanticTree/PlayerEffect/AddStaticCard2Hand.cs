using ExpressionAnalyser;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    [Serializable]
    public class AddStaticCard2Hand : Effect
    {
        [XmlElement(ElementName = "name")]
        public string CardName;
        [XmlElement(ElementName = "num")]
        public string NumExpression;

        private IExpression num;
        public override void Execute()
        {
            int b = num.Value;
            //feature 如果手牌满了则不会加入
            for (int i = 0; i < b && !Context.PlayerContext.IsHandFull; i++)
            {
                Card newCard = GameManager.Instance.CardLibrary.GetCopyByName(CardName);
                Context.PlayerContext.AddCard(PileType.Hand, newCard);
            }
        }
        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
