using System.Collections;
using UnityEngine;

public class TurnControllerEnemyDialog : TurnController
{
    [SerializeField]
    private DialogSystem dialogSystem;

    public override bool EndTurnTrigger
    {
        get
        {
            return dialogSystem.ChoiceTrigger;
        }
    }

    public override void EndTurn()
    {
        for (int i = 0; i < 100; i++)
        {
            if (dialogSystem.NextState != Ink2Unity.InkState.Content) return;
            dialogSystem.MoveNext();
        }
    }

    public void StepIn()
    {
        dialogSystem.MoveNext();
    }
}