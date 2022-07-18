using System.Collections.Generic;
using UnityEngine;
using ModdingAPI;

public class StaticLibraryBase<T> : Singleton<StaticLibraryBase<T>> where T:CardBase,new()
{

    [SerializeField]
    protected Dictionary<string, CardBase> cardDic = new Dictionary<string, CardBase>();

    public void DeclareCard(string name, CardInfo cardInfo)
    {
        Debug.Log($"注册卡牌{cardInfo.Name}");
        if (!cardDic.ContainsKey(name))
        {
            T tmp = new T();
            tmp.Construct(cardInfo);
            cardDic[name] = tmp;
        }

    }

    public List<string> GetAllCards()
    {
        List<string> res = new List<string>();
        foreach (string name in cardDic.Keys)
        {
            res.Add(name);
        }
        return res;
    }

    public List<string> GetRandom(int cnt = 1)
    {
        List<string> allCards = GetAllCards();
        MyMath.Shuffle(allCards);
        return allCards.GetRange(0, cnt);
    }

    public T GetCopyByName(string name)
    {
        CardBase old = cardDic[name];
        var res = new T();
        res.Construct(old);
        return res;
    }

}
