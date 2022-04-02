﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SemanticTree.ChoiceEffects
{
    public class CurrentFocus:Effect
    {
        [XmlElement(ElementName = "action")]
        public EffectList actions;

        public override void Construct()
        {
            actions.Construct();
        }

        public override void Execute()
        {
            SpeechType? focus = Context.PlayerContext.FocusSpeechType;
            if (focus==null) return;
            List<ChoiceSlot> slots= Context.PlayerContext.ChooseSystem.GetChoiceSlot(focus.Value);
            foreach (ChoiceSlot slot in slots)
            {
                Context.choiceSlotStack.Push(slot);
                actions.Execute();
                Context.choiceSlotStack.Pop();
            }
        }
    }
}
