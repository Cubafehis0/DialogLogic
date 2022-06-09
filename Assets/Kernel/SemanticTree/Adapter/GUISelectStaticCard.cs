using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.Adapter
{
    public class GUISelectStaticCard : Effect
    {
        [XmlArray(ElementName = "card_list")]
        public List<string> CardList = null;
        private List<Card> cards = null;

        [XmlElement(ElementName = "num")]
        public string NumExpression = null;
        private IExpression num;

        [XmlElement(ElementName = "action")]
        public EffectList Actions = null;


        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
            CardList.ForEach(x => cards.Add(GameManager.Instance.CardLibrary.GetCopyByName(x)));
            Actions?.Construct();
        }

        public override void Execute()
        {
            var msg = new PileSelectGUIContext(cards, num.Value, Actions);
            GUISystemManager.Instance.Open("w_select_pile", msg);
        }
    }
}
