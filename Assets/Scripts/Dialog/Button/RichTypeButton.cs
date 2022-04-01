using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ink.Runtime;
using System;
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
        if (!hasPlayEnd && coroutine!=null)
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
        StringBuilder builder=new StringBuilder();
        for (int i = 0; i < content.Length; i++)
        {
            //djc: 大量的内存垃圾
            //zc: 使用StringBuilder构建字符串（类似与C++的string）
            if (content[i] != '\r' && content[i] != '\n')
            {
                builder.Append(content[i]);
                txt.text=builder.ToString();
            }
            yield return new WaitForSeconds(.1f);
        }
        hasPlayEnd = true;
    }
}
