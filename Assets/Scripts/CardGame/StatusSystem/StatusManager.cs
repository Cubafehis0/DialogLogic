﻿using UnityEngine;
using UnityEngine.Events;

public enum DMGType
{
    Normal,
    Magic
}



public class StatusManager : MonoBehaviour, IStatusManager
{
    private StatusManagerRaw _statusManager = new StatusManagerRaw();

    private IStatusManager statusManager { get => _statusManager; }
    public IReadonlyModifierGroup Modifiers => statusManager.Modifiers;

    public void AddAnonymousCostModifer(CostModifier costModifier, int timer)
    {
        statusManager.AddAnonymousCostModifer(costModifier, timer);
    }

    public void AddAnonymousFocusModifer(SpeechType speechType, int timer)
    {
        statusManager.AddAnonymousFocusModifer(speechType, timer);
    }

    public void AddAnonymousPersonalityModifier(Personality personality, int timer)
    {
        statusManager.AddAnonymousPersonalityModifier(personality, timer);
    }

    public void AddAnonymousSpeechModifer(SpeechArt speechArt, int timer)
    {
        statusManager.AddAnonymousSpeechModifer(speechArt, timer);
    }

    public void AddStatusCounter(Status status, int value)
    {
        statusManager.AddStatusCounter(status, value);
    }

    public int GetStatusValue(Status status)
    {
        return statusManager.GetStatusValue(status);
    }

    public int GetStatusValue(string name)
    {
        return statusManager.GetStatusValue(name);
    }

    public void OnTurnEnd()
    {
        statusManager.OnTurnEnd();
    }

    public void OnTurnStart()
    {
        statusManager.OnTurnStart();
    }
}





