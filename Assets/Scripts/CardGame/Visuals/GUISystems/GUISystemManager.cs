using SemanticTree;
using SemanticTree.Condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringWindowDictionary : SerializableDictionary<string, ForegoundGUISystem> { }

public class GUISystemManager : MonoBehaviour
{
    [SerializeField]
    public ChooseGUISystem chooseSystem;
    [SerializeField]
    private ForegoundGUISystem tendencySelectGUISystem;
    [SerializeField]
    private ForegoundGUISystem conditionSelectGUISystem;
    [SerializeField]
    private ForegoundGUISystem handSelectGUISystem;
    [SerializeField]
    private ForegoundGUISystem pileSelectGUISystem;
    [SerializeField]
    private ForegoundGUISystem slotGUISystem;
    [SerializeField]
    private ForegoundGUISystem selectLootGUISystem;
    [SerializeField]
    private ForegoundGUISystem dialogChooseGUISystem;
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
        tendencySelectGUISystem = Instantiate(tendencySelectGUISystem, transform);
        conditionSelectGUISystem = Instantiate(conditionSelectGUISystem, transform);
        handSelectGUISystem = Instantiate(handSelectGUISystem, transform);
        pileSelectGUISystem = Instantiate(pileSelectGUISystem, transform);
        slotGUISystem = Instantiate(slotGUISystem, transform);
        selectLootGUISystem = Instantiate(selectLootGUISystem, transform);
        dialogChooseGUISystem = Instantiate(dialogChooseGUISystem, transform);
    }

    public void OpenSlotSelectPanel(EffectList action)
    {
        slotGUISystem.Open(new SlotGUIContext(action));
    }

    public void OpenConditionNerfPanel(int value)
    {
        conditionSelectGUISystem.Open(value);
    }

    public void OpenTendencyChoosePanel(HashSet<PersonalityType> types, int value)
    {
        tendencySelectGUISystem.Open(new TendencyAddGUIContext(types, value, true));
    }

    public void OpenSelectLootGUISystem(List<string> loots)
    {
        selectLootGUISystem.Open(loots);
    }

    public void Open(string key,object msg)
    {
        if (dictionary.TryGetValue(key,out var window))
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