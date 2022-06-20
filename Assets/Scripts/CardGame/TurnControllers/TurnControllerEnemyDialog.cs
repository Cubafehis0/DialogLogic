using UnityEngine;

public class TurnControllerEnemyDialog : TurnController
{
    [SerializeField]
    private DialogSystem dialogSystem;

    public override bool EndTurnTrigger
    {
        get
        {
            return dialogSystem.Blocked;
        }
    }

    public override void EndTurn()
    {
        base.EndTurn();
        for (int i = 0; i < 100; i++)
        {
            if (dialogSystem.InkStory.NextState != Ink2Unity.InkState.Content) return;
            dialogSystem.MoveNext();
        }
    }

    public void StepIn()
    {
        if (CardGameManager.Instance.TurnManager.IsPlayerTurn) return;
        dialogSystem.MoveNext();
    }
}