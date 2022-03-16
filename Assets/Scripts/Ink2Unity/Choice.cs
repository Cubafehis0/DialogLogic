using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{
    public class Choice
    {
        private readonly int index;
        private readonly Content content;
        private SpeechType speechArt = SpeechType.Normal;
        private Personality judgeValue = new Personality(999, 999, 999, 999);
        private int success_desc = 0;
        private int fail_add = 0;


        public Choice(Content content, int index)
        {
            this.content = content;
            this.index = index;
        }

        public int Index => index;
        /// <summary>
        /// 当前选项的内容
        /// </summary>
        public Content Content { get => content; }
        /// <summary>
        /// 当前选项的话术类型
        /// </summary>
        public SpeechType SpeechArt { get => speechArt; set => speechArt = value; }
        /// <summary>
        /// 判定值
        /// </summary>
        public Personality JudgeValue { get => judgeValue; set => judgeValue = value; }
        /// <summary>
        /// 判定成功后压力槽减少至
        /// </summary>
        public int Success_desc { get => success_desc; set => success_desc = value; }
        /// <summary>
        /// 判定失败后压力槽增加值
        /// </summary>
        public int Fail_add { get => fail_add; set => fail_add = value; }


        /// <summary>
        /// 选项框背景色，和选项类型有关
        /// </summary>
        public Color BgColor
        {
            get
            {
                return SpeechArt switch
                {
                    SpeechType.Cheat => Color.yellow,
                    SpeechType.Persuade => Color.green,
                    SpeechType.Threaten => Color.red,
                    SpeechType.Normal => Color.white,
                    _ => Color.white,
                };
            }
        }


    }

}
