using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace zc
{
    public class Choice
    {
        /// <summary>
        /// 当前选项的内容
        /// </summary>
        public Content content;
        /// <summary>
        /// 当前选项是否可用
        /// </summary>
        public bool canUse;
        /// <summary>
        /// 选项框背景色，和选项类型有关
        /// </summary>
        public Color bgColor;
        /// <summary>
        /// 当前选项的话术类型
        /// </summary>
        public SpeechArt speechArt;
        /// <summary>
        /// 当前选项的索引值
        /// </summary>
        public int index;
        public Choice()
        {

        }
        //说话后产生的效果
        public void Effect()
        {

        }
        Color GetSpeechArtColor(SpeechArt art)
        {
            switch (art)
            {
                case SpeechArt.Cheat:
                    return Color.yellow;
                case SpeechArt.Persuade:
                    return Color.green;
                case SpeechArt.Threaten:
                    return Color.red;
                default:
                    return Color.white;
            }

        }
    }

}
