using ExpressionAnalyser;
using SemanticTree.Condition;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SemanticTree.Adapter
{
    public class GUISelectHand : Effect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression = "1";
        private IExpression num;

        [XmlElement(ElementName = "condition")]
        public AllNode Condition = null;

        [XmlElement(ElementName = "action")]
        public EffectList Actions = null;


        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
            Condition?.Construct();
            Actions?.Construct();
        }

        public override void Execute()
        {
            GUISystemManager.Instance.Open("w_select_hand",new HandSelectGUIContext(Context.PlayerContext.Hand, Condition, num.Value, Actions));
        }
    }
}
