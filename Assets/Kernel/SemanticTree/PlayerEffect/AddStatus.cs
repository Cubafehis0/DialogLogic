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
        [XmlElement(ElementName = "target")]
        public TargetTypeEnum TargetType = TargetTypeEnum.FROM;
        [XmlIgnore]
        public bool TargetTypeSpecified { get => TargetType != TargetTypeEnum.FROM; }


        [XmlElement(ElementName = "name")]
        public string StatusName = null;

        [XmlElement(ElementName = "value")]
        public string ValueExpression = null;

        private IExpression value=null;
        private Status status = null;

        public override void Execute()
        {
            switch (TargetType)
            {
                case TargetTypeEnum.FROM:
                    Context.PlayerContext.StatusManager.AddStatusCounter(status, value.Value);
                    break;
                case TargetTypeEnum.TARGET:
                    Context.Target.StatusManager.AddStatusCounter(status, value.Value);
                    break;
            }
        }

        public override void Construct()
        {
            status = StaticStatusLibrary.GetByName(StatusName);
            value = ExpressionParser.AnalayseExpression(ValueExpression);
        }
    }
}
