using ModdingAPI;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{
    [Serializable]
    public class Content
    {
        /// <summary>
        ///  ���ݵĸ��ı��ַ���,��Ҫ��ѡText�����RichText����
        /// </summary>
        public string richText;
        /// <summary>
        /// �����ַ���
        /// </summary>
        public string pureText;
        /// <summary>
        /// 
        /// </summary>
        public SpeakerEnum speaker;
        /// <summary>
        /// ״̬�ı�ֵ������Ϊ��С��߼������¡��ػء�
        /// </summary>
        public Personality personalityModifier;
        /// <summary>
        /// 
        /// </summary>
        public int changeTurn;
        public Content(string content)
        {
            richText = content;
            pureText = TagHandle.GetPureText(richText);
        }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Speaker":
                    speaker = TagHandle.ParseSpeaker(value);
                    return;
                case "StateChange":
                    List<int> a = TagHandle.ParseArray(value);
                    personalityModifier = new Personality(a.GetRange(0, 4));
                    changeTurn = a[4];
                    return;
                default:
                    Debug.LogError("�޷�ʶ��ı�ǩ���ͣ�" + name + ":" + value);
                    return;
            }
        }


        public override string ToString()
        {
            return richText + "   " + speaker.ToString();
        }
    }
}
