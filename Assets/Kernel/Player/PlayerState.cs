using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kernel.Interfaces;

namespace Kernel.Player
{
    internal class PlayerState : IPlayerState
    {
        private IPlayer player;
        private IStatusManager statusManager = null;
        private ICardController cardController = null;

        protected ModifierGroup modifiers = new ModifierGroup();

        public ModifierGroup Modifiers { get => modifiers; }
        public IPlayer Player { get => player; set => player = value; }
    }
}
