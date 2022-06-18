using ModdingAPI;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{
    [Serializable]
    public class Choice
    {
        private int index;
        public Content content;
        public SpeechType speechType = SpeechType.Normal;
        public Personality judgeValue = new Personality(0, 0, 0, 0);
        public int success_desc = 0;
        public int fail_add = 0;


        public Choice(Content content, int index)
        {
            this.content = content;
            this.index = index;
        }

        public int Index { get => index; }
        /// <summary>
        /// 当前选项的内容
        /// </summary>
        public Content Content { get => content; }
        /// <summary>
        /// 当前选项的话术类型
        /// </summary>
        public SpeechType SpeechType { get => speechType; set => speechType = value; }
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

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Speaker":
                    Content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                case "CanUse":
                    List<int> values = TagHandle.ParseArray(value);
                    JudgeValue = new Personality(values);
                    return;
                case "SpeechArt":
                    SpeechType = TagHandle.ParseSpeechArt(value);
                    return;
                case "Success":
                    Success_desc = int.Parse(value);
                    return;
                case "Fail":
                    Fail_add = int.Parse(value);
                    return;
                case "StateChange":
                    List<int> a = TagHandle.ParseArray(value);
                    Content.personalityModifier = new Personality(a.GetRange(0, 4));
                    Content.changeTurn = a[4];
                    return;
                default:
                    Debug.LogError("无法识别的标签类型：" + name + ":" + value);
                    return;
            }
        }

    }

}
