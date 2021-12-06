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

    public DialogSaveAndLoadPanel dialogSaveAndLoadPanel;

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

    private void Start()
    {
        //Debug.Log("start");
        dialogSaveAndLoadPanel = this.GetComponentInChildren<DialogSaveAndLoadPanel>();
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
        //dialogSaveAndLoadPanel.UpdateChoose(); ´ýÊµÏÖ
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
    
}
