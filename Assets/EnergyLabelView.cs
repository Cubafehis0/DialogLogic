using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyLabelView : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        UpdateLabel();
        CardPlayerState.Instance.OnEnergyChange.AddListener(UpdateLabel);
    }

    private void UpdateLabel()
    {
        if(text) text.text = CardPlayerState.Instance.Energy.ToString();
    }
}
