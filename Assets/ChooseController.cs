using Ink2Unity;
using ModdingAPI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseController : MonoBehaviour, IProperties
{
    public List<ChoiceSlot> choices = new List<ChoiceSlot>();

    public void SetChoices(List<Choice> newChoices)
    {
        choices.Clear();
        foreach (Choice choice in newChoices)
        {
            choices.Add(new ChoiceSlot { Choice = choice });
        }
    }

    private class ChoiceConditionPair
    {
        public ChoiceSlot choice;
        public PersonalityType speechType;
    }
    public void RandomReveal(int num)
    {
        List<ChoiceConditionPair> l = new List<ChoiceConditionPair>();
        foreach (ChoiceSlot choice in choices)
        {
            var unmasked = choice.PickupAllUnmasked();
            if (unmasked != null)
            {
                foreach (PersonalityType type in unmasked)
                {
                    l.Add(new ChoiceConditionPair { choice = choice, speechType = type });
                }
            }
        }
        for (int i = 0; i < num && l.Count > 0; i++)
        {
            ChoiceConditionPair luck = l[Random.Range(0, l.Count)];
            luck.choice.AddMask(luck.speechType);
            l.Remove(luck);
        }
    }

    public void RandomReveal(SpeechType type, int num)
    {
        List<ChoiceConditionPair> l = new List<ChoiceConditionPair>();
        foreach (ChoiceSlot choice in choices)
        {
            if (choice.Choice.SpeechType != type) return;
            var unmasked = choice.PickupAllUnmasked();
            if (unmasked != null)
            {
                foreach (PersonalityType p in unmasked)
                {
                    l.Add(new ChoiceConditionPair { choice = choice, speechType = p });
                }
            }
        }
        for (int i = 0; i < num && l.Count > 0; i++)
        {
            ChoiceConditionPair luck = l[Random.Range(0, l.Count)];
            luck.choice.AddMask(luck.speechType);
            l.Remove(luck);
        }
    }

    public List<ChoiceSlot> GetChoiceSlot(SpeechType type)
    {
        return choices.FindAll(x => x.Choice.SpeechType == type);
    }
    public bool TryGetInt(string key, out int value)
    {
        switch (key)
        {
            case "normal_count":
                value = choices.Select(x => x.Choice.SpeechType == SpeechType.Normal).Count();
                return true;
            case "threat_count":
                value = choices.Select(x => x.Choice.SpeechType == SpeechType.Threaten).Count();
                return true;
            case "persuade_count":
                value = choices.Select(x => x.Choice.SpeechType == SpeechType.Persuade).Count();
                return true;
            case "cheat_count":
                value = choices.Select(x => x.Choice.SpeechType == SpeechType.Cheat).Count();
                return true;
            default:
                value = 0;
                return false;
        }
    }
}
