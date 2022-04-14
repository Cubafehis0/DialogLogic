using ExpressionAnalyser;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace SemanticTree.ChoiceEffects
{
    /// <summary>
    /// 
    /// </summary>
    public class AllChoiceRandomReveal : ChoiceEffect
    {
        [XmlElement(ElementName = "num")]
        public string NumExpression = null;
        private IExpression num = null;

        [XmlElement(ElementName = "speech_type")]
        public SpeechType SpeechType = SpeechType.Normal;
        [XmlIgnore]
        public bool SpeechTypeSpecified = false;

        public override void Execute()
        {
            Debug.Log(Context.PlayerContext);

            if (SpeechTypeSpecified) Context.PlayerContext.ChooseGUISystem.RandomReveal(SpeechType, num.Value);
            else Context.PlayerContext.ChooseGUISystem.RandomReveal(num.Value);
        }

        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }
    }
}
