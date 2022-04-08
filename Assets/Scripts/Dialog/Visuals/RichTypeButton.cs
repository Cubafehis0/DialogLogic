using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ink.Runtime;
using System.Text;

public interface IRichTypeButton
{
    void Clear();
    void FastForward();
    void TypeText(string Button_text);
}

public class RichTypeButton : RichButton, IRichTypeButton
{

    public bool hasPlayEnd;

    private string content;
    private Coroutine coroutine;

    protected override void Start()
    {
        btn.onClick.AddListener(FastForward);
    }

    public void TypeText(string Button_text)
    {
        txt.text = null;
        hasPlayEnd = false;
        coroutine = StartCoroutine(ShowText(Button_text));
    }

    public void FastForward()
    {
        if (!hasPlayEnd)
        {
            StopCoroutine(coroutine);
            txt.text = content;
            hasPlayEnd = true;
        }
        else
        {
            OnClick.Invoke(this);
        }
    }

    public void Clear()
    {
        txt.text = null;
    }


    private IEnumerator ShowText(string content)
    {
        this.content = content;
        hasPlayEnd = false;
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] != '\r' && content[i] != '\n')
            {   
                txt.text += content[i];
            }
            yield return new WaitForSeconds(.1f);
        }
        hasPlayEnd = true;
    }
}
