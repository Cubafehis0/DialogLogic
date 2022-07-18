using ModdingAPI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardExample : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public CardControllerBase cardController;
    private List<string> cardSet = new List<string>()
    {
        "distribution",
        "distribution",
        "distribution"
    };
    void Start()
    {
        StaticCardLibrary.Instance.DeclareCard("distribution", null);
        cardController.AddCard<CardBase>(PileType.DrawDeck, cardSet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
