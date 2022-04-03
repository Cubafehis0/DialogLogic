﻿using System.Xml;
using System.Xml.Serialization;

namespace SemanticTree.PlayerEffect
{
    /// <summary>
    /// Effect
    /// 有缺陷,只能常数
    /// Personality参数
    /// </summary>
    public class AnonymousModifyPersonality : Effect
    {
        [XmlElement(ElementName = "personality")]
        public Personality Modifier;

        [XmlElement(ElementName = "duration")]
        public int Timer;

        public override void Execute()
        {
            Context.PlayerContext.StatusManager.AddAnonymousPersonalityModifier(Modifier, Timer);
        }

        public override void Construct()
        {

        }
    }
}