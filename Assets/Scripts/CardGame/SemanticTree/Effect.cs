using SemanticTree.PlayerEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    [XmlInclude(typeof(AddCard2Hand))]
    [XmlInclude(typeof(AddCostModifier))]
    [XmlInclude(typeof(AddStatus))]
    [XmlInclude(typeof(DiscardAllHand))]
    [XmlInclude(typeof(DiscardSomeHandNode))]
    [XmlInclude(typeof(Draw))]
    [XmlInclude(typeof(GUISelectHandNode))]
    [XmlInclude(typeof(GUISelectStaticCard))]
    [XmlInclude(typeof(ModifyFocusNode))]
    [XmlInclude(typeof(ModifyHealth))]
    [XmlInclude(typeof(ModifyPersonality))]
    [XmlInclude(typeof(ModifySpeech))]
    [XmlInclude(typeof(RemoveCostModifier))]
    [XmlInclude(typeof(SetDrawBan))]
    public abstract class Effect:IEffectNode
    {
        public abstract void Construct();
        public abstract void Execute();
    }
}
