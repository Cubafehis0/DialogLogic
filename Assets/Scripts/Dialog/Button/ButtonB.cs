using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ink.Runtime;


public class ButtonB : ButtonScript
{

    private string text_;
    
    public override void ButtonInit(ButtonController buttonController)
    {
        base.ButtonInit(buttonController);
        hasPlayEnd = false;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (hasPlayEnd)
        {

        }
        else
        {
            text.text = text_;
            hasPlayEnd = true;
        }
        //if (m_buttonController.dialogTextLoad)
        //    m_buttonController.dialogTextLoad._storyNeeded = true;
        //else Debug.Log("no m_buttonController.dialogTextLoad");
        //Debug.Log("onclickbuttonB");
        //Destroy(this.gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }

    public override void SetText(string Button_text, bool play)
    {
        if (play)
            StartCoroutine(ShowText(Button_text));
        else
            base.SetText(Button_text, play);
    }
    IEnumerator ShowText(string Button_text)
    {
        text_ = Button_text;

        for (int i = 0; i < Button_text.Length; i++)
        {
            if (hasPlayEnd == false)
                text.text += Button_text[i];
            if (i == Button_text.Length - 1)
                hasPlayEnd = true;
            yield return new WaitForSeconds(.1f);
        }

    }

    public void OnClickDialogButton()
    {
        //RemoveChildren(DialogPanel.gameObject);
        //m_buttonController.dialogTextLoad.SetStroyNeeded(true);
    }
}
