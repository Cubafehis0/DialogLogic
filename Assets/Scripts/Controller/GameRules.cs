using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{

    [SerializeField]
    private Deck handDeck = null;
    [SerializeField]
    private CardSpawnPoint cardSpawn = null;


    private readonly List<int>  testData = new List<int>
        {
            0,
            0,
            1,
            1,
            0,
            1,
            0
        };

    private void Start()
    {
        if (cardSpawn == null) cardSpawn = DrawDeck.instance.GetComponent<CardSpawnPoint>();
        if (handDeck == null) handDeck = HandDeckArrangement.instance.GetComponent<Deck>();
        NoTargetAimer.instance.AddCallback(AimerCallback);
        OneTargetAimer.instance.AddCallback(AimerCallback);

        StartCoroutine(cardSpawn.CreateBunch(testData.ToArray(), OnDrawCardInstantiate));
    }

    private void OnDrawCardInstantiate(CardObject card)
    {
        card.eventHandler = HandDeckArrangement.instance;
        handDeck.Add(card);
    }

    public bool CheckUsableTarget(GameObject target) { return target; }
    private void AimerCallback(CardObject from, GameObject target)
    {
        if (target == null)
        {
            //瞄准被取消了
        }
        else if (CheckUsableTarget(target))
        {
            //目标有效且可以使用

            //卡牌生效效果()

            //进入弃牌堆
            Deck.MigrateTo(from, DiscardArrangement.instance.GetComponent<Deck>());
        }
        else
        {
            //目标有效但由于限制无法使用
        }


    }
}
