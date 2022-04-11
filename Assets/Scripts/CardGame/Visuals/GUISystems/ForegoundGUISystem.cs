using System.Collections;
using UnityEngine;

public class ForegoundGUISystem :MonoBehaviour, IGUISystem
{
    public static ForegoundGUISystem current = null;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void Open(object msg)
    {
        if (current == null)
        {
            current = this;
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError(string.Format("无法打开{0},已有GUISystem {0} 正在运行", GetType().Name, current.GetType().Name));
            return;
        }
    }

    public virtual void Close()
    {
        enabled = false;
        current = null;
    }
}