using Ink2Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using CardGame.Recorder;
using System.Linq;
using SemanticTree;

public class CardEnemyState : MonoBehaviour
{
    [SerializeField]
    protected PlayerPacked player;
    [SerializeField]
    protected TurnController turnController;
    [SerializeField]
    protected StatusManager statusManager = null;
    [SerializeField]
    protected ModifierGroup modifiers = new ModifierGroup();

    public IReadonlyModifierGroup Modifiers { get => modifiers; }
    public PlayerPacked Player { get => player; private set => player = value; }

    public Personality FinalPersonality
    {
        get
        {
            var res = Player.PlayerInfo.Personality + Modifiers.PersonalityLinear;
            return res;
        }
    }

}
