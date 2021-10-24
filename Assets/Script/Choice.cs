using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace zc
{
    public class Choice
    {
        /// <summary>
        /// ��ǰѡ�������
        /// </summary>
        public Content content;
        /// <summary>
        /// ��ǰѡ���Ƿ����
        /// </summary>
        public bool canUse;
        /// <summary>
        /// ѡ��򱳾�ɫ����ѡ�������й�
        /// </summary>
        public Color bgColor;
        /// <summary>
        /// ��ǰѡ��Ļ�������
        /// </summary>
        public SpeechArt speechArt;
        /// <summary>
        /// ��ǰѡ�������ֵ
        /// </summary>
        public int index;
        public Choice()
        {

        }
        //˵���������Ч��
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
