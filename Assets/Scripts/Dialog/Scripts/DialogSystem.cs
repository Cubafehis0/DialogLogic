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

    private InkStory inkStory;

    public InkState NextState { get => inkStory.NextState; }

    public static void Pause()
    {
        Debug.LogWarning("暂停未实现");
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
    /// 不判断是否可选，强制判定成功/失败
    /// </summary>
    /// <param name="choice"></param>
    /// <param name="success"></param>
    public void ForceSelectChoice(Choice choice, bool success)
    {
        GUISystemManager.Instance.chooseSystem.Close();
        //DialogSaveAndLoadPanel.Instance.SaveTextToFile(choice.Content, true);
        inkStory.SelectChoice(choice, success);
        MoveNext();
    }
    public List<Choice> CurrentChoices()
    {
        return inkStory.CurrentChoices();
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
            //List<Choice> choices = inkStory.CurrentChoices();
            //if (choices != null && choices.Count != 0)
            //{
            //    GUISystemManager.Instance.chooseSystem.Open(choices);
            //}
        }
        else if (inkStory.NextState == InkState.Finish)
        {

        }
    }
}