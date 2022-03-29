using SemanticTree;
using SemanticTree.Adapter;
using SemanticTree.CardEffects;
using SemanticTree.GlobalEffect;
using SemanticTree.PlayerEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    [Serializable]
    public class EffectList : IEffect
    {
        [XmlElement(typeof(AnonymousModifyPersonality),ElementName ="modify_personality")]
        [XmlElement(typeof(AddStaticCard2Hand), ElementName = "add_card_to_hand")]
        [XmlElement(typeof(AnonymousModifyCost), ElementName = "add_cost_modifier")]
        [XmlElement(typeof(AddStatus), ElementName = "add_status")]
        [XmlElement(typeof(DiscardAllHand), ElementName = "discard_all_hand")]
        [XmlElement(typeof(DiscardSomeHand), ElementName = "discard_some_hand")]
        [XmlElement(typeof(Draw), ElementName = "draw")]
        [XmlElement(typeof(AnonymousModifyFocus), ElementName = "modify_focus")]
        [XmlElement(typeof(ModifyHealth), ElementName = "modify_health")]
        [XmlElement(typeof(AnonymousModifySpeech), ElementName = "modify_speech")]
        [XmlElement(typeof(RemoveCostModifier), ElementName = "remove_cost_modifier")]
        [XmlElement(typeof(SetDrawBan), ElementName = "set_draw_ban")]

        [XmlElement(typeof(GUISelectDynamicCard),ElementName ="GUI_select_dynamic_card")]
        [XmlElement(typeof(GUISelectStaticCard), ElementName = "GUI_select_static_card")]
        [XmlElement(typeof(RandomDynamicCard),ElementName ="random_dynamic_card")]
        [XmlElement(typeof(ForEachDynamicCard),ElementName ="foreach_dynamic_card")]

        [XmlElement(typeof(ActivateCard),ElementName ="activate")]
        [XmlElement(typeof(AddCopy2HandNode),ElementName ="add_copy_to_hand")]
        [XmlElement(typeof(DiscardCard),ElementName ="discard")]
        [XmlElement(typeof(ExecuteCard),ElementName ="execute")]

        [XmlElement(typeof(IF), ElementName = "if")]
        
        public List<Effect> effects=new List<Effect>();
        public void Execute()
        {
            effects.ForEach(x => x.Execute());
        }

        public void Construct()
        {
            effects.ForEach(x => x.Construct());
        }

        public void Add(Effect effect)
        {
            effects.Add(effect);
        }


        public static implicit operator EffectList(Effect effect)
        {
            return new EffectList { effects=new List<Effect> { effect } };
        }
    }
}
