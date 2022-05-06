using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerPlayerDialog : TurnController
{
    [SerializeField]
    private DialogSystem dialogSystem;
    //public override bool EndTurnTrigger => dialogSystem.NextState != InkState.Choice || tempEndTurnTrigger;
    public override bool EndTurnTrigger => endTurnTrigger || tempEndTurnTrigger;
    private bool endTurnTrigger = false;
    public bool additionalTurn = false;
    private bool tempEndTurnTrigger = false;
    public void SetEndTurnTrigger() { endTurnTrigger = true; }
    public void GetAddditionalTurnAndEndTurn()
    {
        CardGameManager.Instance.playerState.Player.PlayerInfo.Pressure++;
        additionalTurn = true;
        tempEndTurnTrigger = true;
    }

    public override void StartTurn()
    {
        endTurnTrigger = false;
        additionalTurn = false;
        tempEndTurnTrigger = false;
        GUISystemManager.Instance.chooseSystem.Open(CardGameManager.Instance.dialogSystem.CurrentChoices());
        base.StartTurn();
    }
}
