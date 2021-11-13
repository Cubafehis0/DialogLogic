using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeckEventHandler
{
    public void OnDeckUpdate();
    public void OnDeckAdd(CardObject card);
}

public interface ISavable
{
    //将需要保存的信息封装为一个类
    //序列化和反序列化在Save里面进行
    Deck GetSaveInfo();
}
public interface ILoadable
{
    //进行信息读取
    void LoadInfo(Deck deck);
}

public class Deck : MonoBehaviour
{
    /// <summary>
    /// 属性封装
    /// 当前手中的卡牌物体，如有增加减少，需要调用UpdateReference刷新
    /// 无set方法，有get方法，仅提供拷贝
    /// </summary>
    private CardObject[] cardsList;
    public CardObject[] CardsList { get { return (CardObject[])cardsList.Clone(); } private set => cardsList = value; }

    /// <summary>
    /// 目前没有动态添加Deck组件的需求，所以在初始化后不需要更新
    /// </summary>
    private IDeckEventHandler[] handlers;
    private void Awake()
    {
        handlers = GetComponents<IDeckEventHandler>();
    }

    public void UpdateReference()
    {
        cardsList = transform.GetComponentsInChildren<CardObject>();
        if (handlers!=null)
            foreach (IDeckEventHandler handler in handlers)
                handler.OnDeckUpdate();
    }

    public CardObject GetCard(int index)
    {
        if (0 <= index && index < cardsList.Length)
            return cardsList[index];
        return null;
    }

    public CardObject[] GetAllCards()
    {
        return CardsList;
    }

    public void Add(CardObject card)
    {
        card.deck = this;
        card.transform.SetParent(transform,true);
        card.transform.SetAsLastSibling();
        UpdateReference();
        if (handlers != null)
            foreach (IDeckEventHandler handler in handlers)
                handler.OnDeckAdd(card);

    }


    public static void MigrateTo(CardObject card,Deck newDeck)
    {
        if (card == null || newDeck == null) return;
        if (card.deck!=newDeck)
        {
            Deck oldDeck = card.deck;
            newDeck.Add(card);
            if (oldDeck)
            {
                oldDeck.UpdateReference();
            }
        }
    }
}
