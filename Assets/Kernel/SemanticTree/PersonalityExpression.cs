using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    public class PersonalityExpression
    {
        [XmlElement(ElementName = "inside")]
        public string InsideExp;
        private IExpression insideExp;

        [XmlElement(ElementName = "outside")]
        public string OutsideExp;
        private IExpression outsideExp;

        [XmlElement(ElementName = "logic")]
        public string LogicExp;
        private IExpression logicExp;

        [XmlElement(ElementName = "spirital")]
        public string SpiritalExp;
        private IExpression spiritalExp;

        [XmlElement(ElementName = "moral")]
        public string MoralExp;
        private IExpression moralExp;

        [XmlElement(ElementName = "immoral")]
        public string ImmoralExp;
        private IExpression immoralExp;

        [XmlElement(ElementName = "roundabout")]
        public string RoundaboutExp;
        private IExpression roundaboutExp;

        [XmlElement(ElementName = "aggressive")]
        public string AggressiveExp;
        private IExpression aggressiveExp;

        public void Construct()
        {
            if (InsideExp != null) insideExp = ExpressionParser.AnalayseExpression(InsideExp);
            if (OutsideExp != null) outsideExp = ExpressionParser.AnalayseExpression(OutsideExp);
            if (LogicExp != null) logicExp = ExpressionParser.AnalayseExpression(LogicExp);
            if (SpiritalExp != null) spiritalExp = ExpressionParser.AnalayseExpression(SpiritalExp);
            if (MoralExp != null) moralExp = ExpressionParser.AnalayseExpression(MoralExp);
            if (ImmoralExp != null) immoralExp = ExpressionParser.AnalayseExpression(ImmoralExp);
            if (RoundaboutExp != null) roundaboutExp = ExpressionParser.AnalayseExpression(RoundaboutExp);
            if (AggressiveExp != null) aggressiveExp = ExpressionParser.AnalayseExpression(AggressiveExp);
        }

        public Personality Value
        {
            get
            {
                var res = new Personality();
                if (insideExp != null) res.Inner = insideExp.Value;
                if (outsideExp != null) res.Outside = outsideExp.Value;
                if (logicExp != null) res.Logic = logicExp.Value;
                if (spiritalExp != null) res.Spiritial = spiritalExp.Value;
                if (immoralExp != null) res.Immoral = immoralExp.Value;
                if (moralExp != null) res.Moral = moralExp.Value;
                if (roundaboutExp != null) res.Roundabout = roundaboutExp.Value;
                if (aggressiveExp != null) res.Aggressive = aggressiveExp.Value;
                return res;
            }
        }
    }
}
