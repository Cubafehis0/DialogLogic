using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        public Speaker speaker;
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
        public override string ToString()
        {
            return richText +"   "+ speaker.ToString();
        }
    }
}
