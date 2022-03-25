using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SemanticTree.PlayerEffect;
using SemanticTree.ChoiceEffect;
using SemanticTree.CardEffect;
using SemanticTree.Expression;
using System.Xml.Serialization;

namespace SemanticTree
{ 
    public interface IEffectNode
    {
        void Execute();
    }
}
