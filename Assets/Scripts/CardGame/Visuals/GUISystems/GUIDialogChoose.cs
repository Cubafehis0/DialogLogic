using UnityEngine;
public class GUIDialogChoose : MonoBehaviour, IChoiceSlotReciver
{
    public void ChoiceSlotReciver(object msg)
    {
        ChoiceSlot slot = (ChoiceSlot)msg;
        DialogSystem.Instance.ForceSelectChoice(slot.Choice, CardGameManager.Instance.playerState.JudgeChooseSuccess(slot));
    }
}
