using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

        public int Index => index;
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


        /// <summary>
        /// ѡ��򱳾�ɫ����ѡ�������й�
        /// </summary>
        public Color BgColor
        {
            get
            {
                return SpeechType switch
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
