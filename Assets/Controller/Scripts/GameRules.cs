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
            //��׼��ȡ����
        }
        else if (CheckUsableTarget(target))
        {
            //Ŀ����Ч�ҿ���ʹ��

            //������ЧЧ��()

            //�������ƶ�
            Deck.MigrateTo(from, DiscardArrangement.instance.GetComponent<Deck>());
        }
        else
        {
            //Ŀ����Ч�����������޷�ʹ��
        }


    }
}
