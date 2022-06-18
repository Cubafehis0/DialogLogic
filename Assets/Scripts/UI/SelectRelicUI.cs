using ModdingAPI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectRelicUI : MonoBehaviour
{
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

        List<Relic> relics = RelicGameManager.Instance.StaticLib.RandomChooseRelics(grade, personality, RelicGameManager.Instance.RelicLib.ownedRelics);
        Debug.Log("relics's count = " + relics.Count);

        OpenSelectList(relics);


    }

    private void Start()
    {
        CloseSelectList();
    }

    private void OpenSelectList(List<Relic> relics)
    {
        for (int i = 0; i < selectLayout.childCount; i++)
        {
            RelicObj slot = selectLayout.GetChild(i).GetComponent<RelicObj>();
            if (i < relics.Count)
            {
                slot.gameObject.SetActive(true);
                slot.SetRelic(relics[i]);
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }
    }

    private void CloseSelectList()
    {
        for (int i = 0; i < selectLayout.childCount; i++)
        {
            RelicObj slot = selectLayout.GetChild(i).GetComponent<RelicObj>();
            slot.gameObject.SetActive(false);
        }
    }

    public void OnChooseRelic()
    {
        Relic relic = EventSystem.current.currentSelectedGameObject.GetComponent<RelicObj>().relic;
        RelicGameManager.Instance.RelicLib.AddRelic(relic);
        GameObject newOwnedRelic = Instantiate(ownedRelicGO, ownedContent);
        newOwnedRelic.GetComponent<OwnedRelicObj>().SetRelic(relic);
        CloseSelectList();
    }

    public void OnRemoveRelic(Relic relic)
    {
        RelicGameManager.Instance.RelicLib.RemoveRelic(relic);
    }
}