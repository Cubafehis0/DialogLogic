using System.Collections.Generic;
using UnityEngine;
using ModdingAPI;

public class StaticLibraryBase : Singleton<StaticLibraryBase>
{

    [SerializeField]
    protected Dictionary<string, CardBase> cardDic = new Dictionary<string, CardBase>();

    public void DeclareCard<T1>(string name, CardInfo cardInfo) where T1 : CardBase, new()
    {
        //Debug.Log($"注册卡牌{cardInfo.Name}");
        if (!cardDic.ContainsKey(name))
        {
            T1 tmp = new T1();
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

    public T GetCopyByName<T>(string name) where T : CardBase, new()
    {
        CardBase old = cardDic[name];
        var res = new T();
        res.Construct(old);
        return res;
    }

}
