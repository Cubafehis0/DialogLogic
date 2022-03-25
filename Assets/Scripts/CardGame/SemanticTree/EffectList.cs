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
    [Serializable]
    public class EffectList : List<Effect>,IEffectNode
    {
        public void Execute()
        {
            ForEach(x => x.Execute());
        }

        public void Construct()
        {
            ForEach(x => x.Construct());
        }


        public static implicit operator EffectList(Effect effect)
        {
            return new EffectList { effect };
        }
    }
}
