﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.CardEffect
{
    /// <summary>
    /// 无参数
    /// </summary>
    [XmlType()]
    public class ActivateCard : CardNode
    {
        [XmlElement(ElementName = "permanent")]
        public bool Permanent { get; set; }
        public ActivateCard() { }
        public override void Construct() { }
        public override void Execute()
        {
            Context.CardContext.TemporaryActivate = true;
        }
    }
}
