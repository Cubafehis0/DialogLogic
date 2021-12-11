using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface ICharacterView
{
    void UpdateAll();
}


public class CharacterBarView : MonoBehaviour, ICharacterView
{
    [SerializeField]
    private Character character = null;
    [SerializeField]
    private Slider insideSlider = null;
    [SerializeField]
    private Slider logicSlider = null;
    [SerializeField]
    private Slider moralSlider = null;
    [SerializeField]
    private Slider strongSlider = null;

    private void Awake()
    {
        if (character == null) character = GetComponent<Character>();

    }

    private void OnEnable()
    {
        if (character != null) character.OnValueChange.AddListener(UpdateAll);
    }

    private void Start()
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
        if (insideSlider) insideSlider.value = character.Inside;
        if (logicSlider) logicSlider.value = character.Logic;
        if (moralSlider) moralSlider.value = character.Moral;
        if (strongSlider) strongSlider.value = character.Detour;
    }
}
