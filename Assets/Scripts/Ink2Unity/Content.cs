using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{
    public class Content
    {
        /// <summary>
        ///  内容的富文本字符串,需要勾选Text组件的RichText属性
        /// </summary>
        public string richText;
        /// <summary>
        /// 内容字符串
        /// </summary>
        public string pureText;
        /// <summary>
        /// 
        /// </summary>
        public Speaker speaker;
        /// <summary>
        /// 状态改变值，依次为外感、逻辑、道德、迂回。
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
