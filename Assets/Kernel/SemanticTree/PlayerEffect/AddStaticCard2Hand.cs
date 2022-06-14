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
        private string cardName;
        private string numExpression;
        private IExpression num;

        [XmlElement(ElementName = "name")]
        public string CardName { get => cardName; set => cardName = value; }
        [XmlElement(ElementName = "num")]
        public string NumExpression { get => numExpression; set => numExpression = value; }

        public override void Execute()
        {
            int b = num.Value;
            //feature 如果手牌满了则不会加入
            for (int i = 0; i < b && !Context.PlayerContext.CardController.IsHandFull(); i++)
            {
                Context.Console.AddCard("", CardName, PileType.Hand);
            }
        }
        public override void Construct()
        {
            num = ExpressionParser.TryAnalayseExpression(NumExpression, out var res) ? res : new ConstNode(1);
        }
    }
}
