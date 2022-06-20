using ModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel
{
    public class GameRule : IGameRule
    {
        public void RegisterCard(CardInfo info)
        {
            GameManager.Instance.CardLibrary.DeclareCard(info);
        }

        public void RegisterStatus(Status info)
        {
            throw new NotImplementedException();
        }
    }
}
