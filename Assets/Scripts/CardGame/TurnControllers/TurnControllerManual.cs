using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerManual : TurnController
{
    [SerializeField]
    private bool endTurnTrigger = false;
    public override bool EndTurnTrigger => endTurnTrigger;

    public override void StartTurn()
    {
        base.StartTurn();
        endTurnTrigger = false;
    }

    public override void EndTurn()
    {
        base.EndTurn();
        endTurnTrigger = true;
    }
}
