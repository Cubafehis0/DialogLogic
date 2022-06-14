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
            if (Context.PlayerContext.CardController.Hand.Count <= num)
            {
                var cp = new List<Card>(Context.PlayerContext.CardController.Hand);
                foreach (var hand in cp)
                {
                    Context.PlayerContext.CardController.DiscardCard(hand);
                }
            }
            else
            {
                var msg = new HandSelectGUIContext(Context.PlayerContext.CardController.Hand, null, numExpression.Value, new DiscardCard());
                GUISystemManager.Instance.Open("w_select_hand", msg);
            }
        }

        public override void Construct()
        {
            numExpression = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
