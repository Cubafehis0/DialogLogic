using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Interfaces
{
    internal interface IPlayerState
    {
        IPlayer Player { get; set; }
        ModifierGroup Modifiers { get; }
    }
}
