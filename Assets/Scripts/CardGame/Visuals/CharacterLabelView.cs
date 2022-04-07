using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterLabelView : MonoBehaviour
{
    [SerializeField]
    private CardPlayerState character = null;
    [SerializeField]
    private Text insideText = null;
    [SerializeField]
    private Text outsideText = null;
    [SerializeField]
    private Text logicText = null;
    [SerializeField]
    private Text spiritalText = null;
    [SerializeField]
    private Text moralText = null;
    [SerializeField]
    private Text immoralText = null;
    [SerializeField]
    private Text roundaboutText = null;
    [SerializeField]
    private Text strongText = null;

    public void UpdateVisuals()
    {
        if (character == null) return;
        Personality personality = character.FinalPersonality;
        if (insideText) insideText.text = personality.Inner.ToString();
        if (outsideText) outsideText.text = personality.Outside.ToString();
        if (logicText) logicText.text = personality.Logic.ToString();
        if (spiritalText) spiritalText.text = personality.Spritial.ToString();
        if (moralText) moralText.text = personality.Moral.ToString();
        if (immoralText) immoralText.text = personality.Immoral.ToString();
        if (roundaboutText) roundaboutText.text = personality.Roundabout.ToString();
        if (strongText) strongText.text = personality.Roundabout.ToString();
    }

    private void Update()
    {
        UpdateVisuals();
    }
}
