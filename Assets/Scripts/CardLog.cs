using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardLogType
{
    DrawCard,
    PlayCard,
    DiscardCard
}

public class CardLog
{
    class CardProperty
    {
        public string cardName;
        public CardType cardType;
        public bool isActive;

        public CardProperty(Card card)
        {
            this.cardName = card.name;
            this.cardType = card.CardType;
            //this.isActive = card.isActive;
        }
    }
    public uint turn;
    public CardLogType logType;
    CardProperty cardProperty;

    public CardLog(uint turn, CardLogType logType, Card card)
    {
        this.turn = turn;
        this.logType = logType;
        this.cardProperty = new CardProperty(card);
    }

    public bool CardActive { get => cardProperty == null ? false : cardProperty.isActive; }
    public CardType CardType { get => cardProperty == null ? CardType.Lgc : cardProperty.cardType; }

}
