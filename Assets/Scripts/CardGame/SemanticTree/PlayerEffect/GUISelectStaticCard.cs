using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// 有缺陷action = null;
    /// </summary>
    public class GUISelectStaticCard : Effect
    {

        [XmlElement(ElementName = "card_name")]
        public List<string> Names { get; set; }

        [XmlElement(ElementName = "action")]
        public Effect action;

        public GUISelectStaticCard()
        {
            Names = new List<string>();
            action = null;
        }

        public GUISelectStaticCard(List<string> names, Effect action)
        {
            Names = names;
            this.action = action;
            Construct();
        }

        public override void Construct()
        {
            action.Construct();
        }

        public override void Execute()
        {
            List<Card> tmp = new List<Card>();
            foreach (string name in Names)
            {
                tmp.Add(StaticCardLibrary.Instance.GetByName(name));
            }
            CardGameManager.Instance.OpenCardChoosePanel(tmp, Names.Count, action);
        }
    }
}
