using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace zc
{
    public class Content : MonoBehaviour
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
    }
}
