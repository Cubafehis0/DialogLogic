using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.CardEffects
{
    public abstract class CardEffect : Effect { }

    //public class CardEffectList
    //{
    //    [XmlElement()]
    //    public List<Effect> effects = new List<Effect>();
    //    public void Execute()
    //    {
    //        effects.ForEach(x => x.Execute());
    //    }

    //    public void Construct()
    //    {
    //        effects.ForEach(x => x.Construct());
    //    }

    //    public void Add(Effect effect)
    //    {
    //        effects.Add(effect);
    //    }
    //}
}
