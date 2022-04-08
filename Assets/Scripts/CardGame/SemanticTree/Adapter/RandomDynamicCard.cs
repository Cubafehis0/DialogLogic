using ExpressionAnalyser;
using SemanticTree;
using SemanticTree.Condition;
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
    /// 有缺陷action = null;
    /// </summary>
    public class RandomDynamicCard : Effect
    {
        [XmlElement(ElementName = "include")]
        public PileType type = 0;

        [XmlElement(ElementName = "num")]
        public string NumExpression;
        private IExpression num = null;

        [XmlElement(ElementName = "condition")]
        public AllNode Condition = null;

        [XmlElement(ElementName = "action")]
        public EffectList Actions = null;

        public RandomDynamicCard()
        {
            NumExpression = "";
            Actions = new EffectList();
        }

        public override void Execute()
        {
            List<Card> tmp = new List<Card>();
            if ((PileType.Hand & type) > 0) tmp.AddRange(Context.PlayerContext.Hand);
            if ((PileType.DrawDeck & type) > 0) tmp.AddRange(Context.PlayerContext.DrawPile);
            if ((PileType.DiscardDeck & type) > 0) tmp.AddRange(Context.PlayerContext.DiscardPile);
            MyMath.Shuffle(tmp);
            foreach (Card card in tmp.GetRange(0, num.Value))
            {
                Context.PushCardContext(card);
                if (Condition?.Value ?? true) Actions?.Execute();
                Context.PopCardContext();
            }
        }

        public override void Construct()
        { 
            num = ExpressionParser.AnalayseExpression(NumExpression);
            Condition?.Construct();
            Actions.Construct();
        }
    }
}
