using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringWindowDictionary : SerializableDictionary<string, ForegoundGUISystem> { }

public class GUISystemManager : MonoBehaviour
{
    [SerializeField]
    public ChooseGUISystem chooseSystem;
    [SerializeField]
    private Transform dialogChooseGUISystem;
    [SerializeField]
    private StringWindowDictionary dictionary = new StringWindowDictionary();


    private static GUISystemManager instance = null;
    public static GUISystemManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;
        Preload();
    }

    private void Preload()
    {
        dialogChooseGUISystem = Instantiate(dialogChooseGUISystem, transform);
        foreach (var kvp in new Dictionary<string, ForegoundGUISystem>(dictionary))
        {
            dictionary[kvp.Key] = Instantiate(kvp.Value, transform);
        }
    }

    public void Open(string key, object msg)
    {
        if (dictionary.TryGetValue(key, out var window))
        {
            window.Open(msg);
        }
        else
        {
            Debug.Log($"没有名为{key}的弹窗");
        }
    }

    public void BorrowSlots(Transform borrower)
    {
        chooseSystem.transform.SetParent(borrower);
    }

    public void ReturnSlots()
    {
        chooseSystem.transform.SetParent(dialogChooseGUISystem.transform);
    }

}