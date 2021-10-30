using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink2Unity;
public class ButtonController : MonoBehaviour
{
    public GameObject buttonAPrefab;
    public GameObject buttonBPrefab;

    public string choose_text;
    public ButtonA m_buttonA;
    public ButtonB m_buttonB;

    private GameObject DialogChoosePanel;
    private GameObject DialogPanel;
    public GameObject DialogNarratagePanel;

    public DialogTextLoad dialogTextLoad;

    private UIController m_UIController;
    [SerializeField] private Sprite[] spritesOfA;
    [SerializeField] private Sprite spriteOfB;
    public void SetButtonA(ButtonA buttonA)
    {
        m_buttonA = buttonA;
        choose_text = m_buttonA.text.text;
    }
    public void SetButtonB(string text)
    {
        GameObject buttonB = Instantiate(buttonBPrefab);
        buttonB.transform.SetParent(DialogPanel.transform, false);
        m_buttonB = buttonB.GetComponent<ButtonB>();
        m_buttonB.ButtonInit(this);
        m_buttonB.SetText(text, true);
        //Debug.Log(text);
        //Debug.Log(m_buttonB.text.text);
    }

    public void Start()
    {
        //Debug.Log("start");
        dialogTextLoad = this.GetComponentInChildren<DialogTextLoad>();
        DialogChoosePanel = this.transform.Find("DialogChoosePanel").gameObject;
        DialogPanel = this.transform.Find("DialogPanel").gameObject;
        DialogNarratagePanel = this.transform.Find("DialogNarratagePanel").gameObject;

        //if (dialogTextLoad == null)
        //{
        //    Debug.Log("no m_buttonController.dialogTextLoad");
        //}
    }

    public void UpdateChoose()
    {
        dialogTextLoad.UpdateChoose();
    }
    public GameObject CreateButtonB(Content content)
    {
        if (content.richText == null) return null;
        GameObject buttonB = Instantiate(buttonBPrefab);
        //Debug.Log("CreateButtonB:" + content.richText+ content.speaker);

        ButtonScript buttonScript = buttonB.GetComponent<ButtonScript>();
        buttonScript.ButtonInit(this);
        buttonScript.SetText(content.richText, true);

        if (content.speaker != 0)
        {
            buttonScript.image.sprite = spriteOfB;

            buttonB.transform.SetParent(DialogPanel.transform, false);
        }
        else 
        {
            //Debug.Log("is ÅÔ°×À²À²À²");
            buttonB.transform.SetParent(DialogNarratagePanel.transform, false); 
        }


        //HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        //layoutGroup.childForceExpandHeight = false;

        return buttonB;
    }
    public int ChooseSprite(Color color)
    {
        if (color == Color.yellow)
        {
            return 0;
        }
        else if (color == Color.green)
        {
            return 1;
        }
        else if (color == Color.red)
        {
            return 2;
        }
        else if (color == Color.white)
        {
            return 3;        
        }
        return 0;
    }
    public GameObject CreateButtonA(Choice choice)
    {
        if (!choice.canUse)
        {
            Debug.Log("cannot  see");
            return null;
        }
        if (choice.content.richText == null) { return null; }
        GameObject button = Instantiate(buttonAPrefab);
        button.transform.SetParent(DialogChoosePanel.transform, false);
        ButtonScript buttonScript = button.GetComponent<ButtonScript>();
        buttonScript.ButtonInit(this);
        buttonScript.SetText(choice.content.richText, false);
        buttonScript.image.sprite = spritesOfA[ChooseSprite(choice.BgColor)];
        //Debug.Log("SetButtonActive:" + text);
        //HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        //layoutGroup.childForceExpandHeight = false;

        return button;
    }
}
