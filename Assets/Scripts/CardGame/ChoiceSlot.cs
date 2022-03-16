using Ink2Unity;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSlot
{
    private readonly Choice choice;
    private bool locked = false;
    private readonly HashSet<PersonalityType> personalityMask = new HashSet<PersonalityType>();

    public ChoiceSlot(Choice choice)
    {
        this.choice = choice;
    }

    public bool Locked { get => locked; set => locked = value; }
    public Choice Choice { get => choice; }
    public SpeechType SlotType { get => Choice.SpeechArt; }

    public PersonalityType? PickupARandomUnmasked()
    {
        PersonalityType[] randomArray = PickupAllUnmasked();
        return randomArray?[Random.Range(0, randomArray.Length - 1)] ?? null;
    }

    public PersonalityType[] PickupAllUnmasked()
    {
        var candidate = Personality.PositiveSet;
        candidate.ExceptWith(personalityMask);
        if (candidate.Count == 0) return null;
        PersonalityType[] ret = new PersonalityType[candidate.Count];
        candidate.CopyTo(ret);
        return ret;
    }

    public void AddMask(PersonalityType type)
    {
        personalityMask.Add(type);
    }

    public void OpenSelectConditionPanel(PersonalityType[] condition)
    {
        throw new System.NotImplementedException();
    }
}