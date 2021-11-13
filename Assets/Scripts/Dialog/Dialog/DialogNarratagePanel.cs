using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;

public interface IDialogNarratagePanel
{
    /// <summary>
    /// …Ë÷√≈‘∞◊ƒ⁄»›£¨ºÃ≥–Dialog
    /// </summary>
    /// <param name="content"></param>
    void SetContent(Content content);
}
public class DialogNarratagePanel : Dialog, IDialogNarratagePanel
{
    //public DialogController m_dialogController;

    private GameObject narratageDialogButton;
    //private Text narratageDialogText;


    public override void SetContent(Content content)
    {
        HideChildren(this.gameObject);

        narratageDialogButton.SetActive(true);
        ButtonScript buttonScript = narratageDialogButton.GetComponent<ButtonScript>();
        buttonScript.RefreshButton();
        buttonScript.SetText(content.richText, true);

        
    }

    private void OnClickButton(Button button)
    {
        //Debug.Log("click");
        if (button.GetComponent<ButtonScript>().hasPlayEnd)
        {
            //button.GetComponent<ButtonScript>().RefreshButton();
            HideChildren(this.gameObject);
            //button.onClick.RemoveListener(delegate { OnClickButton(button); });
            m_dialogController.ClickDialogNarratagePanel();
        }
    }

    private void Awake()
    {
        HideChildren(this.gameObject);
        m_dialogController = this.GetComponentInParent<DialogController>();

        narratageDialogButton = this.transform.Find("ButtonB").gameObject;
        narratageDialogButton.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        //narratageDialogText = narratageDialogButton.GetComponent<Text>();
        narratageDialogButton.GetComponent<Button>().onClick.AddListener(delegate
        { OnClickButton(narratageDialogButton.GetComponent<Button>()); });
        //HideChildren(this.gameObject);
    }
}
