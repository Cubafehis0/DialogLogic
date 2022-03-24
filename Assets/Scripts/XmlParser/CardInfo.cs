using SemanticTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;



namespace XmlParser
{
    [Serializable]
    public class CardInfo
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "condition_desc")]
        public string ConditionDesc { get; set; }

        [XmlElement(ElementName = "effect_desc")]
        public string EffectDesc { get; set; }

        [XmlElement(ElementName = "meme")]
        public string Meme { get; set; }

        [XmlElement(ElementName = "cost")]
        public int BaseCost { get; set; }

        [XmlIgnore]
        public int category;

        [XmlElement(ElementName = "in_hand_personality")]
        public Personality Personality { get; set; }

        [XmlArray(ElementName = "on_play_card")]
        public EffectList Effects { get; set; }
    }
}
