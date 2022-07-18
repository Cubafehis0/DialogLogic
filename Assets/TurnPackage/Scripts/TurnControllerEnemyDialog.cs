using UnityEngine;

public class TurnControllerEnemyDialog : TurnController
{
    [SerializeField]
    private DialogSystem dialogSystem;

    public override bool EndTurnTrigger
    {
        get
        {
            return dialogSystem.IsBlocked();
        }
    }

    public override void OnEndTurn()
    {
        base.OnEndTurn();
        for (int i = 0; i < 100; i++)
        {
            if (dialogSystem.GetInkStory().NextState != Ink2Unity.InkState.Content) return;
            dialogSystem.MoveNext();
        }
    }

    public void StepIn()
    {
        if (CardGameManager.Instance.TurnManager.IsPlayerTurn) return;
        dialogSystem.MoveNext();
    }
}