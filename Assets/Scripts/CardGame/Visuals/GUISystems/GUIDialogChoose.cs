using UnityEngine;
public class GUIDialogChoose : MonoBehaviour, IChoiceSlotReciver
{

    public void ChoiceSlotReciver(object msg)
    {
        ChoiceSlot slot = (ChoiceSlot)msg;
        CardGameManager.Instance.isPlayerTurn = false;
        CardGameManager.Instance.dialogSystem.ForceSelectChoice(slot.Choice, CardGameManager.Instance.playerState.JudgeChooseSuccess(slot));
        GUISystemManager.Instance.chooseSystem.Close();
    }
}
