using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// effect
    /// </summary>
    public class AddStatus : Effect
    {

        [XmlElement(ElementName = "name")]
        public string StatusName = null;

        [XmlElement(ElementName = "value")]
        public string ValueExpression = null;

        private IExpression value;
        private Status status = null;

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddStatusCounter(status, value.Value);
        }

        public override void Construct()
        {
            status = StaticStatusLibrary.GetByName(StatusName);
            value = ExpressionAnalyser.ExpressionParser.AnalayseExpression(ValueExpression);
        }
    }
}
