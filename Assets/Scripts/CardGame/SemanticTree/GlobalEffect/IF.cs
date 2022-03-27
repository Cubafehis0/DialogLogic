using SemanticTree.Condition;
using System;
using System.Xml.Serialization;

namespace SemanticTree.GlobalEffect
{
    public class IF : Effect
    {

        [XmlElement(ElementName ="condition")]
        public AllNode condition=null;

        [XmlElement(ElementName = "action")]
        public EffectList actions = null;

        public override void Execute()
        {
            if (condition?.Value ?? true) actions?.Execute();
        }

        public override void Construct()
        {
            condition?.Construct();
            actions?.Construct();
        }
    }
}
