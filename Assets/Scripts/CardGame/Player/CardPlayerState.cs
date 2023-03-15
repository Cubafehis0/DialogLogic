using Ink2Unity;
using ModdingAPI;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public interface IProperties
{
    bool TryGetInt(string key, out int value);
}

[RequireComponent(typeof(PlayerPacked))]
public class CardPlayerState : MonoBehaviour, IProperties
{
    private PlayerPacked player;
    private CardController cardController;
    private ChooseGUISystem chooseGUISystem;

    [SerializeField]
    protected StatusController statusManager = null;
    [SerializeField]
    protected ModifierGroup modifiers = new ModifierGroup();

    
    public int Strength;

    //不同判定补正的概率
    private static readonly float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };


    public ModifierGroup Modifiers { get => modifiers; }
    public PlayerPacked Player { get => player; private set => player = value; }

    public Personality GetFinalPersonality()
    {
        var playerPacked = GetComponent<PlayerPacked>();
        var res = playerPacked.Personality + Modifiers.PersonalityLinear + statusManager.Modifiers.PersonalityLinear;
        return res;
    }

    public SpeechArt FinalSpeechArt
    {
        get => Modifiers.SpeechLinear + statusManager.Modifiers.SpeechLinear;
    }
    public SpeechType? FocusSpeechType
    {
        get => Modifiers.Focus ?? statusManager.Modifiers.Focus;
    }


    public StatusController StatusManager => statusManager;

    public bool CanChoose(ChoiceSlot slot)
    {
        //判断是否可选
        return !(slot.Locked || (FocusSpeechType != null && FocusSpeechType == slot.Choice.SpeechType));
    }

    private void Awake()
    {
        player = GetComponent<PlayerPacked>();
        cardController = GetComponent<CardController>();
    }

    /// <summary>
    /// 不判断是否可选，强制选择选项
    /// </summary>
    /// <param name="slot"></param>
    public bool JudgeChooseSuccess(ChoiceSlot slot)
    {
        int dis = Personality.MaxDistance(GetFinalPersonality(), slot.Choice.JudgeValue);
        SpeechArt speech = FinalSpeechArt;
        int modifier = slot.Choice.SpeechType switch
        {
            SpeechType.Normal => speech[SpeechType.Normal],
            SpeechType.Cheat => speech[SpeechType.Cheat],
            SpeechType.Threaten => speech[SpeechType.Threaten],
            SpeechType.Persuade => speech[SpeechType.Persuade],
            _ => 0,
        };
        int randomEPS = MyMath.GetRandomJudge(jp);
        return dis <= randomEPS + modifier;
    }

    public void AddAnonymousPersonalityModifier(Personality personality, int timer = -1, DMGType type = DMGType.Normal)
    {
        if (timer == 0) return;
        if (type == DMGType.Magic)
        {
            personality.Strengthen(Strength);
            Strength = 0;
        }
        if (timer < 0)
        {
            modifiers.AddAnonymousPersonality(personality);
        }
        else
        {
            statusManager.AddAnonymousPersonalityModifier(personality, timer);
        }
    }

    private int GetStatusProp(string name)
    {
        return StatusManager.GetStatusValue(name);
    }

    public bool TryGetInt(string key, out int value)
    {
        var components = GetComponents<IProperties>();
        foreach (var component in components)
        {
            if (component.TryGetInt(key, out value))
            {
                return true;
            }
        }
        value = default;
        return false;
    }
}
