using UnityEngine;
public class GUIDialogChoose : MonoBehaviour, IChoiceSlotReciver
{
    private bool endTurnTrigger = false;
    public bool EndTurnTrigger
    {
        get { return endTurnTrigger; }
    }

    public void ChoiceSlotReciver(object msg)
    {
        ChoiceSlot slot = (ChoiceSlot)msg;
        CardGameManager.Instance.isPlayerTurn = false;
        CardGameManager.Instance.dialogSystem.ForceSelectChoice(slot.Choice, CardGameManager.Instance.playerState.JudgeChooseSuccess(slot));
        endTurnTrigger = true;
    }

    public void StartTurn()
    {
        endTurnTrigger = false;
    }
}
