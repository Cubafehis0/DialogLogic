using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.Adapter
{
    public class SetPileContext : Effect
    {
        [XmlElement]
        public List<string> CardList { get; set; }

        [XmlElement(ElementName = "action")]
        public EffectList Effects { get; set; }

        private List<Card> cards=null;

        public SetPileContext()
        {
            CardList = new List<string>();
        }

        public SetPileContext(EffectList effect, List<string> cards)
        {
            this.Effects = effect;
            this.CardList = cards;
        }

        public override void Construct()
        {
            cards = new List<Card>();
            CardList.ForEach(card => cards.Add(StaticCardLibrary.Instance.GetByName(card)));
        }

        public override void Execute()
        {
            Context.PushPileContext(cards);
            Effects.Execute();
            Context.PopPileContext();
        }
    }
}
