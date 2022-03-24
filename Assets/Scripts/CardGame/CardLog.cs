
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardLogType
{
    DrawCard,
    PlayCard,
    DiscardCard
}

public interface ICardLogProperty
{
    string name { get; }
    int cardCategory { get; }
    bool IsActive { get; }
}

public class CardLog
{
    private uint turn;
    private CardLogType logType;
    private ICardLogProperty card;

    public CardLog(uint turn, CardLogType logType, ICardLogProperty card)
    {
        this.turn = turn;
        this.logType = logType;
        this.card = card;
    }

    public uint Turn { get => turn; }
    public CardLogType LogType { get => logType; }
    public string CardName { get => card?.name ?? ""; }
    public bool CardActive { get => card?.IsActive ?? false; }
    //public int CardCategory { get => card?.cardCategory ?? CA.Lgc; }
}
