using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    public class SpeechArtExpression
    {
        [XmlElement(ElementName = "normal")]
        public string NormalExp;
        private IExpression normalExp;

        [XmlElement(ElementName = "cheat")]
        public string CheatExp;
        private IExpression cheatExp;

        [XmlElement(ElementName = "threat")]
        public string ThreatExp;
        private IExpression threatExp;

        [XmlElement(ElementName = "persuade")]
        public string PersuadeExp;
        private IExpression persuadeExp;

        public void Construct()
        {
            if (NormalExp != null) normalExp = ExpressionParser.AnalayseExpression(NormalExp);
            if (CheatExp != null) cheatExp = ExpressionParser.AnalayseExpression(CheatExp);
            if (ThreatExp != null) threatExp = ExpressionParser.AnalayseExpression(ThreatExp);
            if (PersuadeExp != null) persuadeExp = ExpressionParser.AnalayseExpression(PersuadeExp);
        }

        public SpeechArt Value
        {
            get
            {
                var res = new SpeechArt();
                if (normalExp != null) res.Normal = normalExp.Value;
                if (cheatExp != null) res.Cheat = cheatExp.Value;
                if (persuadeExp != null) res.Threat = persuadeExp.Value;
                if (threatExp != null) res.Persuade = threatExp.Value;
                return res;
            }
        }
    }
}
