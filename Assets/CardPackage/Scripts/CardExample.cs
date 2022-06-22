using ModdingAPI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardExample : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public CardController cardController;
    [SerializeField]
    private Kernel.GameRule gameRule;
    private List<string> cardSet = new List<string>()
    {
        "distribution",
        "distribution",
        "distribution"
    };
    void Start()
    {
        Context.Console = new Kernel.GameConsole();
        Context.Query = new Kernel.GameQuery();
        Context.Rule = gameRule;
        LoadAllCommon(Path.Combine(Application.streamingAssetsPath, "CardLib"));
        cardController.AddCard<Card>(PileType.DrawDeck, cardSet);
    }

    private void LoadAllCommon(string path)
    {
        JasperMod.Loader.LoadAllCommon(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
