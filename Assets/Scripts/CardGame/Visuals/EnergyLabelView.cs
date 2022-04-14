using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyLabelView : MonoBehaviour
{
    [SerializeField]
    private CardPlayerState player;
    [SerializeField]
    private Text text;

    private void OnEnable()
    {
        player.OnEnergyChange.AddListener(UpdateLabel);
    }

    private void OnDisable()
    {
        player.OnEnergyChange.RemoveListener(UpdateLabel);
    }

    private void Start()
    {
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (text) text.text = player.Energy.ToString();
    }
}
