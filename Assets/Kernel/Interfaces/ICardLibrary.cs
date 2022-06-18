using ModdingAPI;
using System.Collections.Generic;

public interface ICardLibrary
{
    Card CopyCard(Card card);
    void DeclareCard(CardInfo cardInfo);
    void DeclareCard(List<CardInfo> cardInfos);
    Card GetCopyByName(string name);

    List<string> GetAllCards();
    List<string> GetRandom(int cnt);
}