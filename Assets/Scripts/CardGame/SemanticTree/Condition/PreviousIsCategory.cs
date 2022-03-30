using CardGame.Recorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.Condition
{
    public class PreviousIsCategory : ConditionNode
    {
        [XmlElement]
        public CardCategory category;

        public override bool Value
        {
            get
            {
                var qs = from x in CardRecorder.Instance.cardLogs
                         where x.Turn == CardGameManager.Instance.turn
                         && x.LogType == CardLogEntryEnum.PlayCard
                         select x;
                if (qs.Count() == 0) return false;
                return qs.First().CardCategory == category;
            }
        }

        public override void Construct()
        {
        }
    }
}
