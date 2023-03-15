using ModdingAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRule : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    private void SetupCardSet()
    {
        foreach (string card in GameManager.Instance.LocalPlayer.CardSet)
        {
            GameConsole.Instance.AddCard(card, PileType.DrawDeck, PilePositionType.Random);
        }
        GameConsole.Instance.ShufflePile(PileType.DrawDeck);
    }

    private void Start()
    {
        SetupCardSet();
    }
}
