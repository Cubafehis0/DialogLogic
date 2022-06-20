using ModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel
{
    public class GameQuery : IGameQuery
    {
        public string GetCardName(string id)
        {
            throw new NotImplementedException();
        }

        public List<string> GetCards(string tag, ModdingAPI.PileType pileType)
        {
            throw new NotImplementedException();
        }

        public int GetPileCount(string target, ModdingAPI.PileType pileType)
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
    }
}
