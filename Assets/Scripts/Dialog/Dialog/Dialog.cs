using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
public interface IDialog
{
    /// <summary>
    /// ����parent�µ����к���
    /// </summary>
    /// <param name="parent"></param>
    public void HideChildren(GameObject parent);

    /// <summary>
    /// �ڶ�Ӧ��Panel����������
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(Content content);
}
public abstract class Dialog : MonoBehaviour
{
    protected DialogController m_dialogController;
    protected virtual void HideChildren()
    {
        var childCount = this.transform.childCount;
        for (int i = 0; i < childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(false);

    }

    //public virtual void SetContent(Content content) { }
}
