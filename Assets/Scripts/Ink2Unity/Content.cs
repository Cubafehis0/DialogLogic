using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{
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
        public Speaker speaker;
        /// <summary>
        /// ״̬�ı�ֵ������Ϊ��С��߼������¡��ػأ������غ�����
        /// </summary>
        public List<int> stateChange;
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
