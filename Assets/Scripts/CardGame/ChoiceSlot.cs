using Ink2Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChoiceSlot
{
    [SerializeField]
    private Choice choice;
    [SerializeField]
    private bool locked = false;
    [SerializeField]
    private HashSet<PersonalityType> revealMask = new HashSet<PersonalityType>();

    public bool Locked { get => locked; set => locked = value; }
    public Choice Choice { 
        get => choice;
        set
        {
            choice = value ?? throw new ArgumentNullException(nameof(value));
            RevealMask.Clear();
        }
    }

    public HashSet<PersonalityType> RevealMask { get => revealMask;}

    public PersonalityType? PickupARandomUnmasked()
    {
        PersonalityType[] randomArray = PickupAllUnmasked();
        return randomArray?[UnityEngine.Random.Range(0, randomArray.Length - 1)] ?? null;
    }

    public PersonalityType[] PickupAllUnmasked()
    {
        var candidate = Personality.PositiveSet;
        candidate.ExceptWith(RevealMask);
        if (candidate.Count == 0) return null;
        PersonalityType[] ret = new PersonalityType[candidate.Count];
        candidate.CopyTo(ret);
        return ret;
    }

    public void AddMask(PersonalityType type)
    {
        RevealMask.Add(type);
    }

    public void OpenSelectConditionPanel(PersonalityType[] condition)
    {
        throw new System.NotImplementedException();
    }
}