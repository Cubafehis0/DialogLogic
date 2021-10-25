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
