using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChoiceSlotObject : MonoBehaviour
{
    [SerializeField]
    private List<ChoiceConditionObject> ConditionIcons;
    [SerializeField]
    private ChoiceSlot choiceSlot;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        
    }
    public void UpdateVisuals()
    {
        button.interactable = choiceSlot.Locked;
        Personality judgeValue = choiceSlot.Choice.JudgeValue;
        int cnt = 0;
        ConditionIcons.ForEach(icon =>icon.gameObject.SetActive(false));

        foreach(PersonalityType x in Enum.GetValues(typeof(PersonalityType)))
        {
            if (judgeValue[x] > 0)
            {
                ConditionIcons[cnt].Type = x;
                ConditionIcons[cnt].Value = judgeValue[x];
                ConditionIcons[cnt].Reveal = choiceSlot.RevealMask.Contains(x);
                ConditionIcons[cnt].gameObject.SetActive(false);
                ConditionIcons[cnt].UpdateVisuals();
                cnt++;
            }
        }
    }
}