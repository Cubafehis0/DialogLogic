using ModdingAPI;
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


    private void Update()
    {
        UpdateAll();
    }

    public void UpdateAll()
    {
        if (character == null) return;
        Personality personality = character.GetFinalPersonality();
        if (insideSlider) insideSlider.value = personality.Inner;
        if (logicSlider) logicSlider.value = personality.Logic;
        if (moralSlider) moralSlider.value = personality.Moral;
        if (strongSlider) strongSlider.value = personality.Aggressive;
        if (outsideSlider) outsideSlider.value = personality.Outside;
        if (passionSlider) passionSlider.value = personality.Spiritial;
        if (unethicSlider) unethicSlider.value = personality.Immoral;
        if (detourSlider) detourSlider.value = personality.Roundabout;
    }
}
