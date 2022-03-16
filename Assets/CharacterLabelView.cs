using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IPersonalityGet
{
    UnityEvent OnValueChange { get; }

    Personality Personality { get; }
}
public class CharacterLabelView : MonoBehaviour, ICharacterView
{
    [SerializeField]
    private IPersonalityGet character = null;
    [SerializeField]
    private Text insideText = null;
    [SerializeField]
    private Text logicText = null;
    [SerializeField]
    private Text moralText = null;
    [SerializeField]
    private Text strongText = null;

    private void Awake()
    {
        if (character == null) character = GetComponent<IPersonalityGet>();

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
        Personality personality = character.Personality;
        if (insideText) insideText.text = personality.Inside.ToString();
        if (logicText) logicText.text = personality.Logic.ToString();
        if (moralText) moralText.text = personality.Moral.ToString();
        if (strongText) strongText.text = personality.Detour.ToString();
    }
}
