using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLabelView : MonoBehaviour, ICharacterView
{
    [SerializeField]
    private Character character = null;
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
        if (insideText) insideText.text = character.Inside.ToString();
        if (logicText) logicText.text = character.Logic.ToString();
        if (moralText) moralText.text = character.Moral.ToString();
        if (strongText) strongText.text = character.Detour.ToString();
    }
}
