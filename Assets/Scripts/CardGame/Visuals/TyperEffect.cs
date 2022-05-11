using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;

public interface ITyperEffect
{
    void Clear();
    void FastForward();
    void TypeText(string content);
}

public class TyperEffect : MonoBehaviour, ITyperEffect
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private UnityEvent OnClick = new UnityEvent();

    private bool hasPlayEnd;
    private string content;
    private Coroutine coroutine;

    public void TypeText(string Button_text)
    {
        text.text = "";
        hasPlayEnd = false;
        coroutine = StartCoroutine(ShowText(Button_text));
    }

    public void FastForward()
    {
        if (!hasPlayEnd)
        {
            StopCoroutine(coroutine);
            text.text = content;
            hasPlayEnd = true;
        }
        else
        {
            OnClick.Invoke();
        }
    }

    public void Clear()
    {
        text.text = null;
    }


    private IEnumerator ShowText(string content)
    {
        this.content = content;
        hasPlayEnd = false;
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] != '\r' && content[i] != '\n')
            {
                text.text += content[i];
            }
            yield return new WaitForSeconds(.1f);
        }
        hasPlayEnd = true;
    }
}
