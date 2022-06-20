using System.Collections.Generic;

public interface ICardController
{
    IReadonlyPile<Card> DiscardPile { get; }
    IReadonlyPile<Card> DrawPile { get; }
    IReadonlyPile<Card> Hand { get; }
    bool DrawBan { get; set; }
    bool IsHandFull();
    bool CanDraw();

    void AddCard(PileType pileType, string name);
    void AddCard(PileType pileType, Card card);
    void AddCard(PileType pileType, IEnumerable<string> names);
    void AddCard(PileType pileType, IEnumerable<Card> cards);

    void DiscardAll();
    void DiscardCard(Card cid);
    void Draw(int num = 1);
    void Draw2Full();
    int? GetPileProp(string name);
    void PlayCard(Card card);

}
