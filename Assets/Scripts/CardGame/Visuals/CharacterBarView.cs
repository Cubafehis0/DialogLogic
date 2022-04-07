using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBarView : MonoBehaviour
{
    [SerializeField]
    private CardPlayerState character = null;
    [SerializeField]
    private Slider insideSlider = null;
    [SerializeField]
    private Slider logicSlider = null;
    [SerializeField]
    private Slider moralSlider = null;
    [SerializeField]
    private Slider strongSlider = null;
    [SerializeField]
    private Slider outsideSlider = null;
    [SerializeField]
    private Slider passionSlider = null;
    [SerializeField]
    private Slider unethicSlider = null;
    [SerializeField]
    private Slider detourSlider = null;

    private void OnEnable()
    {
        if (character != null) character.OnValueChange.AddListener(UpdateAll);
    }

    private void Update()
    {
        UpdateAll();
    }

    private void OnDisable()
    {
        if (character != null) character.OnValueChange.RemoveListener(UpdateAll);
    }
    public void UpdateAll()
    {
        if (character == null) return;
        Personality personality = character.FinalPersonality;
        if (insideSlider) insideSlider.value = personality.Inner;
        if (logicSlider) logicSlider.value = personality.Logic;
        if (moralSlider) moralSlider.value = personality.Moral;
        if (strongSlider) strongSlider.value = personality.Aggressive;
        if (outsideSlider) outsideSlider.value = personality.Outside;
        if (passionSlider) passionSlider.value = personality.Spritial;
        if (unethicSlider) unethicSlider.value = personality.Immoral;
        if (detourSlider) strongSlider.value = personality.Roundabout;
    }
}
