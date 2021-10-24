using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace zc
{
    public class Content : MonoBehaviour
    {

        /// <summary>
        ///  内容的富文本字符串,需要勾选Text组件的RichText属性
        /// </summary>
        public string richText;
        /// <summary>
        /// 内容字符串
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
