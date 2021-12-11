using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface ICardPlayerStateObject
{
    void OnEnergyChange();
    void OnDrawButHandFull();
    void OnDrawButEmpty();
    void OnDraw();
    void OnDiscard();
    void OnUse();
    void OnTurnStart();
}

[RequireComponent(typeof(ICardPlayerState))]
public class CardPlayerStateObject : MonoBehaviour,ICardPlayerStateObject
{
    private ICardPlayerState playerState=null;

    [SerializeField]
    private Text energyText;

    private void Awake()
    {
        playerState = GetComponent<ICardPlayerState>();
    }

    private void Start()
    {
        OnEnergyChange();
    }

    private void OnEnable()
    {
        playerState.UpdateObjectReference();
    }

    private void OnDisable()
    {
        playerState.UpdateObjectReference();
    }

    public void OnEnergyChange()
    {
        if (energyText) energyText.text = playerState.Energy.ToString();
    }

    public void OnDraw()
    {
        return;
    }

    public void OnDiscard()
    {
        return;
    }

    public void OnUse()
    {
        return;
    }

    public void OnTurnStart()
    {
        return;
    }

    public void OnDrawButHandFull()
    {
        return;
    }

    public void OnDrawButEmpty()
    {
        return;
    }
}
