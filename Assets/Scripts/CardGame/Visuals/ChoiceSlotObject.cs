using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ChoiceSlotObject : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image image;
    [SerializeField]
    private ChoiceSlot choiceSlot = null;
    [SerializeField]
    private List<ChoiceConditionObject> ConditionIcons;

    public ChoiceSlot ChoiceSlot { get { return choiceSlot; } set { choiceSlot = value; } }

    private void Awake()
    {
        if(button==null) button = GetComponentInChildren<Button>();
        if (text == null) text = GetComponentInChildren<Text>();
        if (image == null) image = GetComponentInChildren<Image>();
    }
    public void UpdateVisuals()
    {
        if (choiceSlot == null) return;
        if (text) text.text = choiceSlot.Choice.Content.richText;
        if (image) image.sprite = GameManager.Instance.ChoiceSprites[choiceSlot.Choice.SpeechType];
        button.interactable = !choiceSlot.Locked;
        UpdateJudge();
    }

    public void UpdateJudge()
    {
        Personality judgeValue = choiceSlot.Choice.JudgeValue;
        int cnt = 0;
        foreach (PersonalityType x in Enum.GetValues(typeof(PersonalityType)))
        {
            if (cnt >= ConditionIcons.Count) break;
            if (judgeValue[x] > 0)
            {
                ConditionIcons[cnt].Type = x;
                ConditionIcons[cnt].Value = judgeValue[x];
                ConditionIcons[cnt].Reveal = choiceSlot.RevealMask.Contains(x);
                ConditionIcons[cnt].gameObject.SetActive(true);
                ConditionIcons[cnt].UpdateVisuals();
                cnt++;
            }
        }
        while (cnt < ConditionIcons.Count)
        {
            ConditionIcons[cnt].gameObject.SetActive(false);
            cnt++;
        }
    }

    private void Update()
    {
        UpdateVisuals();
    }
}