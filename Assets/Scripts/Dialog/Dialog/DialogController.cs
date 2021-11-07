using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
public class DialogController : MonoBehaviour
{
    private InkStory inkUnity;
    [SerializeField] private TextAsset textFile;

    public DialogPanel m_dialogPanel;
    public DialogChoosePanel m_dialogChoosePanel;
    public DialogNarratagePanel m_dialogNarratagePanel;

    public ButtonController m_buttonController;
    private bool canUseDialogPanel;
    private bool canUseDialogChoosePanel;
    private bool canUseDialogNarratagePanel;


    public void ClickDialogPanel()
    {
        canUseDialogPanel = true;
    }

    public void ClickDialogChoosePanel(Choice choice)
    {
        canUseDialogChoosePanel = true;        
        Content content = inkUnity.SelectChoice(choice);
    }

    public void ClickDialogNarratagePanel()
    {
        canUseDialogNarratagePanel = true;
    }

    private void Start()
    {
        canUseDialogChoosePanel = canUseDialogNarratagePanel = canUseDialogPanel = true;
        inkUnity = new InkStory(textFile);
        m_dialogPanel = this.transform.Find("DialogPanel").GetComponent<DialogPanel>();
        m_dialogChoosePanel = this.transform.Find("DialogChoosePanel").GetComponent<DialogChoosePanel>();
        m_dialogNarratagePanel = this.transform.Find("DialogNarratagePanel").GetComponent<DialogNarratagePanel>();
        m_buttonController = this.GetComponent<ButtonController>();
    }

    private void Update()
    {
        if (m_dialogPanel == null)
        {
            Debug.Log("null");
        }
        if (m_dialogChoosePanel == null)
        {
            Debug.Log("null");
        }
        if (m_dialogNarratagePanel == null)
        {
            Debug.Log("null");
        }
        if (m_buttonController == null)
        {
            Debug.Log("null");
        }

        if (inkUnity.CanCoutinue)
        {
            Content content = inkUnity.CurrentContent();
            List<Choice> choices = inkUnity.CurrentChoices();
            if (content != null)
            {
                if (content.speaker == Speaker.Dialogue && canUseDialogNarratagePanel)
                {
                    m_dialogNarratagePanel.SetContent(content);
                    canUseDialogNarratagePanel = false;

                }
                else if ((content.speaker == Speaker.Player || content.speaker == Speaker.NPC) && canUseDialogPanel)
                {
                    m_dialogPanel.SetContent(content);
                    canUseDialogPanel = false;
                }
                else if (!(content.speaker == Speaker.Player || content.speaker == Speaker.NPC || content.speaker == Speaker.Dialogue))
                {
                    Debug.LogError("wrong speakerType");
                }
            }
            else
            {
                Debug.Log("content is null");
            }
            if (choices != null)
                m_dialogChoosePanel.ShowChooseButtons(choices);
            else Debug.Log("qq");
        }
    }

}
