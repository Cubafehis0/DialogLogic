using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerPlayerDialog : TurnController
{
    [SerializeField]
    private DialogSystem dialogSystem;
    public override bool EndTurnTrigger => dialogSystem.NextState != InkState.Choice;

    public override void EndTurn()
    {
        
    }

    public override void StartTurn()
    {
        base.StartTurn();
        GUISystemManager.Instance.chooseSystem.Open(CardGameManager.Instance.dialogSystem.CurrentChoices());
    }
}
