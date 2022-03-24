using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlParser;

namespace SemanticTree.CardEffect
{
    [XmlInclude(typeof(CardCategory)),
    XmlInclude(typeof(ExecuteCard)),
    XmlInclude(typeof(DiscardCard)),
    XmlInclude(typeof(ActivateCard)),
    XmlInclude(typeof(ActivateCard))]
    public abstract class CardNode : Effect
    {

    }
}
