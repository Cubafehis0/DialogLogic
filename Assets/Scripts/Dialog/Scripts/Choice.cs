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
        /// ��ǰѡ�������
        /// </summary>
        public Content Content { get => content; }
        /// <summary>
        /// ��ǰѡ��Ļ�������
        /// </summary>
        public SpeechType SpeechType { get => speechType; set => speechType = value; }
        /// <summary>
        /// �ж�ֵ
        /// </summary>
        public Personality JudgeValue { get => judgeValue; set => judgeValue = value; }
        /// <summary>
        /// �ж��ɹ���ѹ���ۼ�����
        /// </summary>
        public int Success_desc { get => success_desc; set => success_desc = value; }
        /// <summary>
        /// �ж�ʧ�ܺ�ѹ��������ֵ
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
                    Debug.LogError("�޷�ʶ��ı�ǩ���ͣ�" + name + ":" + value);
                    return;
            }
        }

    }

}
