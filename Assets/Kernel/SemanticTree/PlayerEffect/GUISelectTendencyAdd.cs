using ExpressionAnalyser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    public class GUISelectTendencyAdd:Effect
    {
        [XmlElement(ElementName = "include")]
        public HashSet<PersonalityType> types;
        [XmlElement(ElementName = "value")]
        public string NumExpression;
        private IExpression num;

        public override void Construct()
        {
            num = ExpressionParser.AnalayseExpression(NumExpression);
        }

        public override void Execute()
        {
            GUISystemManager.Instance.OpenTendencyChoosePanel(types, num.Value);
        }
    }
}
