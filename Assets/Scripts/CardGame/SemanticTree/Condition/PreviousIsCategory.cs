using CardGame.Recorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class PreviousIsCategory:ConditionNode
    {
        [XmlElement]
        public CardCategory category;

        public override bool Value
        {
            get
            {
                var qs=CardRecorder.Instance.Query(CardLogFindScopeEnum.ThisTurn, 0, x => true).ToList();
                return qs[0].CardCategory==category;
            }
        }

        public override void Construct()
        {
        }
    }
}
