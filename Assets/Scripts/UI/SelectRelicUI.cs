using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectRelicUI : MonoBehaviour
{
    public GameObject selctRelicGO;
    public Transform selectLayout;

    public GameObject ownedRelicGO;
    public Transform ownedContent;

    public InputField gradeInputField;
    public InputField logicInputField;
    public InputField passionInputField;
    public InputField moralInputField;
    public InputField unethicInputField;
    public InputField detourInputField;
    public InputField strongInputField;

    public void OnSelect()
    {
        int grade;
        Personality personality = new Personality();
        int tempWeight;
        personality[PersonalityType.Logic] = personality[PersonalityType.Moral] =
            personality[PersonalityType.Detour] = 0;
        if (gradeInputField.text == string.Empty || !int.TryParse(gradeInputField.text, out grade))
            grade = 0;
        if (logicInputField.text != string.Empty && int.TryParse(logicInputField.text, out tempWeight))
            personality[PersonalityType.Logic] = tempWeight;
        if (passionInputField.text != string.Empty && int.TryParse(passionInputField.text, out tempWeight))
            personality[PersonalityType.Passion] = tempWeight;
        if (moralInputField.text != string.Empty && int.TryParse(moralInputField.text, out tempWeight))
            personality[PersonalityType.Moral] = tempWeight;
        if (unethicInputField.text != string.Empty && int.TryParse(unethicInputField.text, out tempWeight))
            personality[PersonalityType.Unethic] = tempWeight;
        if (detourInputField.text != string.Empty && int.TryParse(detourInputField.text, out tempWeight))
            personality[PersonalityType.Detour] = tempWeight;
        if (strongInputField.text != string.Empty && int.TryParse(strongInputField.text, out tempWeight))
            personality[PersonalityType.Strong] = tempWeight;

        Debug.Log("Grade = " + grade);
        Debug.Log("Logic = " + personality[PersonalityType.Logic]);
        Debug.Log("Passion = " + personality[PersonalityType.Passion]);
        Debug.Log("Moral = " + personality[PersonalityType.Moral]);
        Debug.Log("Unethic = " + personality[PersonalityType.Unethic]);
        Debug.Log("Detour = " + personality[PersonalityType.Detour]);
        Debug.Log("Strong = " + personality[PersonalityType.Strong]);

        List<Relic> relics = GameManager.Instance.RelicLib.RandomChooseRelics(grade, personality);
        Debug.Log("relics's count = " + relics.Count);

        for (int i = 0; i < selectLayout.childCount; i++)
        {
            Destroy(selectLayout.GetChild(i).gameObject);
        }

        foreach (Relic relic in relics)
        {
            GameObject newSelectRelic = Instantiate<GameObject>(selctRelicGO, selectLayout);
            newSelectRelic.GetComponent<RelicObj>().SetRelic(relic);
        }
    }

    public void OnReset()
    {
        GameManager.Instance.RelicLib.ResetRelics();
        for (int i = 0; i < ownedContent.childCount; i++)
        {
            Destroy(ownedContent.GetChild(i).gameObject);
        }
    }

    public void OnChooseRelic(Relic relic)
    {
        GameManager.Instance.RelicLib.AddRelic(relic);
        GameObject newOwnedRelic = Instantiate(ownedRelicGO, ownedContent);
        newOwnedRelic.GetComponent<OwnedRelicObj>().SetRelic(relic);
        //newOwnedRelic.transform.Find("Name").GetComponent<Text>().text = relic.relicInfo.Name;
        //newOwnedRelic.transform.Find("Description").GetComponent<Text>().text = relic.relicInfo.Description;
        if (ownedContent.childCount > 8)
        {
            RectTransform rectTransform = ownedContent.GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width + 190);
        }
        for (int i = 0; i < selectLayout.childCount; i++)
        {
            Destroy(selectLayout.GetChild(i).gameObject);
        }
    }

    public void OnRemoveRelic(Relic relic)
    {
        GameManager.Instance.RelicLib.RemoveRelic(relic);
        if (ownedContent.childCount >= 8)
        {
            RectTransform rectTransform = ownedContent.GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width - 190);
        }
    }
}