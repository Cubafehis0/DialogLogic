using System;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.GlobalEffect
{
    public class IF : Effect
    {
        private ICondition condition;
        [XmlElement(ElementName = "action")]
        public EffectList Actions { get; set; }

        public IF(ICondition condition, EffectList action)
        {
            this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
            this.Actions = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Execute()
        {
            if (condition.Value) Actions.Execute();
        }

        public override void Construct()
        {

        }
    }
}
