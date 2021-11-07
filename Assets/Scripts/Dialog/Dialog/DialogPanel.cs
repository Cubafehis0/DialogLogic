using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;

public class DialogPanel : Dialog
{
    public DialogController m_dialogController;

    private GameObject NPC_Dialog;
    private GameObject Lead_Dialog;

    public void Start()
    {
        m_dialogController = this.GetComponentInParent<DialogController>();
        NPC_Dialog = this.transform.Find("NPCDialog").gameObject;
        Lead_Dialog = this.transform.Find("LeadDialog").gameObject;
        HideChildren(this.gameObject);
    }
    
    public void SetContent(Content content)
    {
        if (content.speaker == Speaker.NPC)
        {
            ShowNPCDialog(content);
        }
        else if (content.speaker == Speaker.Player)
        {
            ShowLeadDialog(content);
        }
        else Debug.LogError("error type speaker in dialogPanel");
    }
    private void OnClickDialogButton(Button button)
    {
        if (button.GetComponent<ButtonScript>().hasPlayEnd)
        {
            HideChildren(this.gameObject);
            button.onClick.RemoveListener(delegate { OnClickDialogButton(button); });

            m_dialogController.ClickDialogPanel();
        }
    }
    private void ShowNPCDialog(Content content)
    {
        NPC_Dialog.SetActive(true);
        ButtonB buttonB = NPC_Dialog.GetComponent<ButtonB>();
        buttonB.ButtonInit(m_dialogController.m_buttonController);
        buttonB.SetText(content.richText, true);

        Button button = NPC_Dialog.GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClickDialogButton(button); });

    }

    private void ShowLeadDialog(Content content)
    {
        Lead_Dialog.SetActive(true);
        ButtonB buttonB = NPC_Dialog.GetComponent<ButtonB>();
        buttonB.ButtonInit(m_dialogController.m_buttonController);
        buttonB.SetText(content.richText, true);

        Button button = NPC_Dialog.GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClickDialogButton(button); });
    }

    private void Update()
    {
        if (NPC_Dialog == null)
        {
            Debug.LogError("null npcdialog");
        }
        if (Lead_Dialog == null)
        {
            Debug.LogError("null Lead_Dialog");
        }
    }

}
