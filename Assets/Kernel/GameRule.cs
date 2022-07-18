using ModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Kernel
{
    public class GameRule : IGameRule
    {
        public void RegisterCard(CardInfo info)
        {
            StaticCardLibrary.Instance.DeclareCard(info.Name,info);
        }

        public void RegisterStatus(Status info)
        {
            throw new NotImplementedException();
        }
    }
}
