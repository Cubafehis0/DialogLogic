using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerPlayerDialog : TurnController
{
    public override bool EndTurnTrigger => endTurnTrigger || tempEndTurnTrigger;
    private bool endTurnTrigger = false;
    private bool tempEndTurnTrigger = false;
    public void SetEndTurnTrigger() { endTurnTrigger = true; }

    public override bool AdditionalTurn => tempEndTurnTrigger;
    public void GetAddditionalTurnAndEndTurn()
    {
        CardGameManager.Instance.playerState.Player.PlayerInfo.Pressure++;
        tempEndTurnTrigger = true;
    }

    public override void StartTurn()
    {
        endTurnTrigger = false;
        tempEndTurnTrigger = false;
        GUISystemManager.Instance.chooseSystem.Open(CardGameManager.Instance.dialogSystem.InkStory.CurrentChoices);
        base.StartTurn();
    }
}
