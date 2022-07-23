using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class AnonymousModifyCost : Effect
    {
        [XmlElement(ElementName = "modifier")]
        public CostModifier Modifier;

        [XmlElement(ElementName = "duration")]
        public string DurationExp;

        private IExpression duration;
        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousCostModifer(Modifier, duration.Value);
        }

        public override void Construct()
        {
            Modifier.Construct();
            duration=ExpressionParser.AnalayseExpression(DurationExp);
        }
    }
}
