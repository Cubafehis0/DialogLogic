using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
public class InkUnityStory : MonoBehaviour
{
    private InkStory inkStory;
    [SerializeField] private TextAsset textFile;

    private DialogController m_dialogController;
    private void Start()
    {
        inkStory = new InkStory(textFile);
        m_dialogController = this.GetComponent<DialogController>();
        m_dialogController.inkStory = inkStory;
    }

    private void Update()
    {
        if (inkStory.CanCoutinue)
        {
            SetContent();
        }
        
        SetChoice();

    }
    private void SetChoice()
    {
        List<Choice> choices = inkStory.CurrentChoices();

        if (choices != null && choices.Count != 0 && m_dialogController.canUseDialogChoosePanel&&m_dialogController.canContinue)
        {
            Debug.Log("SetChoice"+choices.Count);
            m_dialogController.SetChoice(choices);
        }
    }
    private void SetContent()
    {
        if (m_dialogController.canContinue)
        {
            Content content = inkStory.CurrentContent();
            m_dialogController.SetContent(content);
            //Debug.Log(content.speaker + content.richText);
        }
    }
}
