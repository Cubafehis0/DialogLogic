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
        public SpeakerEnum speaker;
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

        public void SetValue(string name,string value)
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
                    Debug.LogError("无法识别的标签类型：" + name + ":" + value);
                    return;
            }
        }


        public override string ToString()
        {
            return richText +"   "+ speaker.ToString();
        }
    }
}
