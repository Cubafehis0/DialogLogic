﻿using SemanticTree.PlayerEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{
    [XmlInclude(typeof(ModifyPersonalityNode))]
    public abstract class Effect:IEffectNode
    {
        public abstract void Construct();
        public abstract void Execute();
    }
}
