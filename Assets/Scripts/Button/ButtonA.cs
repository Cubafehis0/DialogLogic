using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ink.Runtime;

public class ButtonA : ButtonScript
{
    public float biggerScale = 1.1f;
    public override void ButtonInit(ButtonController buttonController)
    {
        //Debug.Log("button");
        base.ButtonInit(buttonController);
        hasPlayEnd = true;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale *= biggerScale;

        //Debug.Log("enter");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale /= biggerScale;
        //Debug.Log("exit");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {

        //m_buttonController.SetButtonA(this);
        //if (m_buttonController.m_buttonA)
        //InformButtonB();
        //Debug.Log("no m_buttonController.m_buttonA");
        if (canChoose)
            StartCoroutine(Destroy());
    }

    public void InformButtonB()
    {
        m_buttonController.SetButtonB(m_buttonController.choose_text);

    }
    IEnumerator Destroy()
    {
        //Debug.Log("destroy");
        for (float i = 0f; i <= 1f; i += 0.1f)
        {
            //Debug.Log("destroy" + i);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - i);

            image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - i);
            if (image.color.a < 0.1f) 
            {
                //hasDestroyed = true;
                Destroy(this.gameObject, 0f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //public void OnClickChoiceButton(Choice choice)
    //{
    //    RemoveChildren(DialogChoosePanel.gameObject);

    //    _inkStory.ChooseChoiceIndex(choice.index);
    //    m_dialogTextSave.SaveTextToFile(m_dialogTextSave.textSaveFile, choice.text);
    //    _inkStory.Continue();
    //    //RemoveChildren(DialogPanel.gameObject);
    //    //ShowDialog();
    //    _storyNeeded = true;
    //}
}
