using ExpressionAnalyser;
using SemanticTree.CardEffects;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class DiscardSomeHand : Effect
    {

        [XmlElement(ElementName = "num")]
        public string NumExpression { get; set; }
        private IExpression numExpression;

        public override void Execute()
        {
            int num = numExpression.Value;
            if (Context.PlayerContext.Hand.Count <= num)
            {
                var cp=new List<Card>( Context.PlayerContext.Hand);
                foreach (var hand in cp)
                {
                    Context.PlayerContext.DiscardCard(hand);
                }
            }
            else
            {
                CardGameManager.Instance.OpenHandChoosePanel(null, numExpression.Value, new DiscardCard());
            }
        }

        public override void Construct()
        {
            numExpression = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
