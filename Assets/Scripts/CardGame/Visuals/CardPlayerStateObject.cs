using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardPlayerState))]
public class CardPlayerStateObject : MonoBehaviour
{
    private CardPlayerState playerState=null;

    [SerializeField]
    private Text energyText;

    private void Awake()
    {
        playerState = GetComponent<CardPlayerState>();
    }

    private void Start()
    {
        OnEnergyChange();
    }

    private void OnEnable()
    {
        playerState.OnEnergyChange.AddListener(OnEnergyChange);
    }

    private void OnDisable()
    {
        playerState.OnEnergyChange.RemoveListener(OnEnergyChange);
    }

    public void OnEnergyChange()
    {
        if (energyText) energyText.text = playerState.Energy.ToString();
    }
}
