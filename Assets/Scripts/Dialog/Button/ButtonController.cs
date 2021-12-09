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

    public ButtonB buttonB
    {
        get { return m_buttonB; }
        set
        {
            if (buttonB != null && value == null)
                m_buttonB = value;
            if (buttonB == null)
                m_buttonB = value;
        }
    }
    private void Start()
    {
        //Debug.Log("start");
        dialogSaveAndLoadPanel = this.GetComponentInChildren<DialogSaveAndLoadPanel>();
        DialogChoosePanel = this.transform.Find("DialogChoosePanel").gameObject;
        DialogPanel = this.transform.Find("DialogPanel").gameObject;
        DialogNarratagePanel = this.transform.Find("DialogNarratagePanel").gameObject;
    }


}
