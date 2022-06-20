using ModdingAPI;
using UnityEngine;
using UnityEngine.Events;

public class GUIDialogChoose : MonoBehaviour, IChoiceSlotReciver
{
    public UnityEvent OnChoose = new UnityEvent();
    public void ChoiceSlotReciver(object msg)
    {
        ChoiceSlot slot = (ChoiceSlot)msg;
        //CardGameManager.Instance.isPlayerTurn = false;
        SpeechType? focus = CardGameManager.Instance.playerState.FocusSpeechType;
        if (focus != null && focus != slot.Choice.SpeechType)
        {
            Debug.Log("有锁定选项");
        }
        else
        {
            OnChoose.Invoke();
            CardGameManager.Instance.dialogSystem.ForceSelectChoice(slot.Choice, CardGameManager.Instance.playerState.JudgeChooseSuccess(slot));
            GUISystemManager.Instance.chooseSystem.Close();
        }
    }
}
