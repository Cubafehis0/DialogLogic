using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActorState : MonoBehaviour
{
    [SerializeField]
    protected Player player;
    [SerializeField]
    protected TurnController turnController;
    [SerializeField]
    protected StatusManager statusManager = null;
    [SerializeField]
    protected ModifierGroup modifiers = new ModifierGroup();

    public IReadonlyModifierGroup Modifiers { get => modifiers; }
    public Player Player { get => player; private set => player = value; }

    public Personality FinalPersonality
    {
        get
        {
            var res = Player.PlayerInfo.Personality + Modifiers.PersonalityLinear;
            return res;
        }
    }


}
