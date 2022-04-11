using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDialogSystem
{
    void MoveNext();
}
public class DialogSystem : MonoBehaviour, IDialogSystem
{ 
    [SerializeField]
    private SpeakSystem speakSystem;


    private static DialogSystem instance = null;
    public static DialogSystem Instance { get => instance; }

    private InkStory inkStory;

    private void Awake()
    {
        instance = this;
    }

    public static void Pause()
    {
        Debug.LogWarning("��ͣδʵ��");
    }

    public void Open(TextAsset textAsset)
    {
        if (textAsset != null)
        {
            inkStory = new InkStory(textAsset);
            MoveNext();
        }
        else
        {
            throw new System.ArgumentNullException();
        }
    }

    /// <summary>
    /// ���ж��Ƿ��ѡ��ǿ���ж��ɹ�/ʧ��
    /// </summary>
    /// <param name="choice"></param>
    /// <param name="success"></param>
    public void ForceSelectChoice(Choice choice, bool success)
    {
        GUISystemManager.Instance.chooseSystem.Close();
        CardGameManager.Instance.EndTurn();
        //DialogSaveAndLoadPanel.Instance.SaveTextToFile(choice.Content, true);
        inkStory.SelectChoice(choice, success);
        MoveNext();

    }


    public void MoveNext()
    {

        if (CardGameManager.Instance.isPlayerTurn) return;
        if (inkStory.NextState == InkState.Content)
        {
            Content content = inkStory.NextContent(); ;
            speakSystem.Speak(content.richText, content.speaker);
            //DialogSaveAndLoadPanel.Instance.SaveTextToFile(content);
        }
        else if (inkStory.NextState == InkState.Choice)
        {
            List<Choice> choices = inkStory.CurrentChoices();
            if (choices != null && choices.Count != 0)
            {
                GUISystemManager.Instance.chooseSystem.Open(choices);
                CardGameManager.Instance.StartTurn();
            }
        }
        else if (inkStory.NextState == InkState.Finish)
        {

        }
    }
}