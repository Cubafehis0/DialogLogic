using UnityEngine;
using System.Collections.Generic;
public class RelicsController : MonoBehaviour,ICardControllerEventListener<Card>,ITurnStart,ITurnEnd
{
    [SerializeField]
    private List<Relic> relics = new List<Relic>();

    public void AddRelic(Relic item)
    {
        relics.Add(item);
        item.OnPickUp();
    }

    public IReadOnlyList<Relic> GetRelics()
    {
        return relics;
    }

    private void Start()
    {
        GameConsole.Instance.ObtainRelic("test");
    }

    public void OnDiscard(Card card)
    {
        foreach (Relic relic in relics)
        {
            relic.OnDiscard(card);
        }
    }

    public void OnDiscard2Draw()
    {
        foreach (Relic relic in relics)
        {
            relic.OnDiscard2Draw();
        }
    }

    public void OnDraw(Card card)
    {
        foreach(Relic relic in relics)
        {
            relic.OnDraw(card);
        }
    }

    public void OnEnergyChange()
    {
        foreach (Relic relic in relics)
        {
            relic.OnEnergyChange();
        }
    }

    public void OnPlayCard(Card card)
    {
        foreach(Relic relic in relics)
        {
            relic.OnPlayCard(card);
        }
    }

    public void OnTurnEnd()
    {
        foreach (Relic relic in relics)
        {
            relic.OnTurnEnd();
        }
    }

    public void OnTurnStart()
    {
        foreach (Relic relic in relics)
        {
            relic.OnTurnStart();
        }
    }
}
