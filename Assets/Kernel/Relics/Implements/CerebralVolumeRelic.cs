using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Kernel.Relics.Implements
{
    public class CerebralVolumeRelic : Relic
    {
        CerebralVolumeRelic()
        {
            Name = "cerebral_volume";
            Title = "大脑容量";
            Description = "对话判定的基础值从0变为-2，你获得的每一个对话判定效果持续时间加4回合";
            Rarity = 0;
            Category = ModdingAPI.PersonalityType.Logic;
            InitNum = 0;
            counter = 0;
            modifier = null;
        }

        public override object Clone()
        {
            return new CerebralVolumeRelic();
        }

        public override void OnBattleStart()
        {
            GameConsole.Print("对话判定的基础值从0变为-2");
            GameConsole.Instance.AddStatus("self", "你获得的每一个对话判定效果持续时间加X回合", 4);
        }
    }
}
