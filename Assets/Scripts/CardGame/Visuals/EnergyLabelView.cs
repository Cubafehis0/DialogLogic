using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyLabelView : MonoBehaviour
{
    [SerializeField]
    private CardPlayerState player;

    private Text text;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void OnEnable()
    {
        player.OnEnergyChange.AddListener(UpdateLabel);
        UpdateLabel();
    }

    private void OnDisable()
    {
        player.OnEnergyChange.RemoveListener(UpdateLabel);
    }

    private void UpdateLabel()
    {
        if (text) text.text = player.Energy.ToString();
    }
}
