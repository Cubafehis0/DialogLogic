﻿using SemanticTree.PlayerEffect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree
{

    public abstract class Effect:IEffect
    {
        public abstract void Construct();
        public abstract void Execute();
    }
}