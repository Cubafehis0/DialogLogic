using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
public interface IDialog
{
    /// <summary>
    /// 隐藏parent下的所有孩子
    /// </summary>
    /// <param name="parent"></param>
    public void HideChildren(GameObject parent);

    /// <summary>
    /// 在对应的Panel内设置内容
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(Content content);
}
public abstract class Dialog : MonoBehaviour, IDialog
{
    protected DialogController m_dialogController;
    public virtual void HideChildren(GameObject parent)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            parent.transform.GetChild(i).gameObject.SetActive(false);

    }

    public virtual void SetContent(Content content) { }
}
