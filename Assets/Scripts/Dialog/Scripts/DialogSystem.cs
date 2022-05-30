using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDialogSystem
{
    IReadOnlyStory InkStory { get; }
    void Open(TextAsset textAsset);
    void ForceSelectChoice(Choice choice, bool success);
    void MoveNext();

}

public class DialogSystem : MonoBehaviour, IDialogSystem
{
    [SerializeField]
    private SpeakSystem speakSystem;
    [SerializeField]
    private InkStory inkStory;
    private bool blocked = false;
    public IReadOnlyStory InkStory => inkStory;
    public bool Blocked => blocked;
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
        inkStory.SelectChoice(choice, success);
        blocked = false;
        MoveNext();
    }


    public void MoveNext()
    {
        if (inkStory.NextState == InkState.Content)
        {
            Content content = inkStory.NextContent(); ;
            speakSystem.Speak(content.richText, content.speaker);
        }
        else
        {
            blocked = true;
        }
    }
}