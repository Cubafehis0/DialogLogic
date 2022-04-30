using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActorState : MonoBehaviour
{
    [SerializeField]
    protected PlayerPacked player;
    [SerializeField]
    protected TurnController turnController;
    [SerializeField]
    protected StatusManager statusManager = null;
    [SerializeField]
    protected ModifierGroup modifiers = new ModifierGroup();

    public ModifierGroup Modifiers { get => modifiers; }
    public PlayerPacked Player { get => player; private set => player = value; }

    public Personality GetFinalPersonality()
    {
        var res = Player.PlayerInfo.Personality + Modifiers.PersonalityLinear;
        return res;
    }


}
