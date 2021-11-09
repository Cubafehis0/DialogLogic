using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;
public interface IDialogPanel
{
    /// <summary>
    /// 继承实现Dialog的SetContent，设置内容
    /// </summary>
    /// <param name="content"></param>
   void SetContent(Content content);

}

public class DialogPanel : Dialog, IDialogPanel
{
    //public DialogController m_dialogController;

    private GameObject NPC_Dialog;
    private GameObject Player_Dialog;


    public override void SetContent(Content content)
    {
        HideChildren(this.gameObject);
        if (content.speaker == Speaker.NPC)
        {
            //NPC_Dialog.GetComponent<ButtonScript>().RefreshButton();
            ShowNPCDialog(content);
        }
        else if (content.speaker == Speaker.Player)
        {
            //Player_Dialog.GetComponent<ButtonScript>().RefreshButton();

            ShowPlayerDialog(content);
        }
        else Debug.LogError("error type speaker in dialogPanel");
    }

    private void Start()
    {
        m_dialogController = this.GetComponentInParent<DialogController>();
        NPC_Dialog = this.transform.Find("NPCDialog").gameObject;
        Player_Dialog = this.transform.Find("PlayerDialog").gameObject;
        NPC_Dialog.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        Player_Dialog.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        NPC_Dialog.GetComponent<Button>().onClick.AddListener(delegate { OnClickDialogButton(NPC_Dialog.GetComponent<Button>()); });
        Player_Dialog.GetComponent<Button>().onClick.AddListener(delegate { OnClickDialogButton(Player_Dialog.GetComponent<Button>()); });

        HideChildren(this.gameObject);
    }
    private void OnClickDialogButton(Button button)
    {
        if (button.GetComponent<ButtonScript>().hasPlayEnd)
        {
            //HideChildren(this.gameObject);
            //button.onClick.RemoveListener(delegate { OnClickDialogButton(button); });
            m_dialogController.ClickDialogPanel();
            button.onClick.RemoveAllListeners();
        }
    }
    private void ShowNPCDialog(Content content)
    {
        NPC_Dialog.SetActive(true);
        ButtonB buttonB = NPC_Dialog.GetComponent<ButtonB>();
        //buttonB.ButtonInit(m_dialogController.m_buttonController);
        buttonB.RefreshButton();
        buttonB.SetText(content.richText, true);

        Button button = NPC_Dialog.GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClickDialogButton(button); });
    }

    private void ShowPlayerDialog(Content content)
    {
        Player_Dialog.SetActive(true);
        ButtonB buttonB = Player_Dialog.GetComponent<ButtonB>();
        //buttonB.ButtonInit(m_dialogController.m_buttonController);
        buttonB.RefreshButton();
        buttonB.SetText(content.richText, true);

        Button button = Player_Dialog.GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClickDialogButton(button); });
    }



}
