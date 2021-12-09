using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IContentPanel
{

}
public class ContentPanel : Dialog, IContentPanel
{
    private GameObject narratageDialogButton;
    private GameObject NPC_Dialog;
    private GameObject Player_Dialog;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="richText">说话内容（富文本）</param>
    /// <param name="speaker">说话者</param>
    public  void SetContent(string richText, Speaker speaker)
    {
        if (speaker == Speaker.NPC)
        {
            ShowNPCDialog(richText);
        }
        else if (speaker == Speaker.Player)
        {
            ShowPlayerDialog(richText);
        }
        else if (speaker == Speaker.Dialogue)
        {
            ShowNarratageDialog(richText);
        }
        else Debug.LogError("error type speaker in dialogPanel");
    }
    private void Start()
    {
        HideChildren();
        m_dialogController = this.GetComponentInParent<DialogController>();
        NPC_Dialog = this.transform.Find("NPCDialog").gameObject;
        Player_Dialog = this.transform.Find("PlayerDialog").gameObject;
        NPC_Dialog.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        Player_Dialog.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        NPC_Dialog.GetComponent<Button>().onClick.AddListener(delegate { OnClickDialogButton(NPC_Dialog.GetComponent<Button>()); });
        Player_Dialog.GetComponent<Button>().onClick.AddListener(delegate { OnClickDialogButton(Player_Dialog.GetComponent<Button>()); });

        
        narratageDialogButton = GameObject.Find("DialogNarratagePanel").transform.Find("ButtonB").gameObject;
        narratageDialogButton.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        narratageDialogButton.GetComponent<Button>().onClick.AddListener(delegate{ OnClickButton(narratageDialogButton.GetComponent<Button>()); });
        narratageDialogButton.SetActive(false);
    }

    private void OnClickButton(Button button)
    {
        //Debug.Log("click");
        if (button.GetComponent<ButtonScript>().hasPlayEnd)
        {
            narratageDialogButton.SetActive(false);
            m_dialogController.ClickDialogNarratagePanel();
        }
    }

    private void OnClickDialogButton(Button button)
    {
        if (button.GetComponent<ButtonScript>().hasPlayEnd)
        {
            //HideChildren(this.gameObject);
            //button.onClick.RemoveListener(delegate { OnClickDialogButton(button); });
            m_dialogController.ClickDialogPanel();
            button.gameObject.SetActive(false);
            //button.onClick.RemoveAllListeners();
        }
    }
    private void ShowNPCDialog(string richText)
    {
        Player_Dialog.SetActive(false);
        NPC_Dialog.SetActive(true);
        ButtonB buttonB = NPC_Dialog.GetComponent<ButtonB>();
        buttonB.SetText(richText, true);

    }

    private void ShowPlayerDialog(string richText)
    {
        NPC_Dialog.SetActive(false);
        Player_Dialog.SetActive(true);
        ButtonB buttonB = Player_Dialog.GetComponent<ButtonB>();
        buttonB.SetText(richText, true);
    }

    private void ShowNarratageDialog(string richText)
    {
        narratageDialogButton.SetActive(true);
        ButtonScript buttonScript = narratageDialogButton.GetComponent<ButtonScript>();
        buttonScript.SetText(richText, true);
    }
}
