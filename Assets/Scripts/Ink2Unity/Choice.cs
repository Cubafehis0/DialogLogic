using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
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
        public Color BgColor
        {
            get
            {
                switch (speechArt)
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


        /// <summary>
        /// 当前选项是否可见
        /// </summary>
        public bool isVisible;

        /// <summary>
        /// 当前选项是否被锁定
        /// </summary>
        public bool isLocked;

        //public bool IsLocked
        //{
        //    get
        //    {
        //        return this.isLocked;
        //    }
        //}
        //private bool isLocked;
        //public void Locked()
        //{
        //    isLocked = true;
        //}


        
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
            speechArt = SpeechArt.Normal;
            canUse = true;
        }
        public override string ToString()
        {
            string s = "   ";
            return index + s + content.ToString() + s + speechArt.ToString() + s + BgColor + s + canUse;
        }
    }

}
