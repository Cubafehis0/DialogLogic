using SemanticTree;
using SemanticTree.PlayerEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    
    public class EffectList : IEffectNode
    {
        [XmlArrayItem(typeof(ModifyPersonality),ElementName = "modify_personality")]
        [XmlArrayItem(typeof(AddCard2Hand),ElementName = "add_card2hand")]
        public List<Effect> effects = new List<Effect>();
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
            return new EffectList { effects=new List<Effect> { effect }  };
        }
    }
}
