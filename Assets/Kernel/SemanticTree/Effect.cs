using SemanticTree.PlayerEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
namespace SemanticTree
{

    public abstract class Effect : IEffect
    {
        public static int EffectNodeCnt = 0;
        public Effect() 
        {
            //Debug.Log("效果节点数" + EffectNodeCnt++);
        }

        public abstract void Construct();
        public abstract void Execute();
    }
}
