using ModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameQuery : Singleton<GameQuery>, IGameQuery
{
    public string GetCardName(string id)
    {
        throw new NotImplementedException();
    }

    public int GetCardCost(string id)
    {
        return 0;
    }
    public List<string> GetCards(string tag, PileType pileType)
    {
        throw new NotImplementedException();
    }

    public int GetPileCount(string target, PileType pileType)
    {
        throw new NotImplementedException();
    }

    public int GetPlayerProp(string target, string prop)
    {
        throw new NotImplementedException();
    }

    public bool IsHandFull(string target)
    {
        throw new NotImplementedException();
    }

    public Personality GetPersonality() { throw new NotImplementedException(); }
}