using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
namespace zc
{
    /// <summary>
    /// 工具类，封装了一些Ink提供的API
    /// </summary>
    public class Ink2Unity
    {
        Story story;
        public Ink2Unity(TextAsset inkJSON)
        {
            story = new Story(inkJSON.text);
        }
        /// <summary>
        /// 故事是否可继续读取内容(Content)
        /// </summary>
        public bool CanCoutinue()
        {
            return story.canContinue;
        }
        /// <summary>
        /// 当前故事的内容
        /// </summary>
        public Content CurrentContent()
        {
            string ct = story.Continue();
            Content rs = new Content(ct);
            List<string> tags = story.currentTags;
            if(tags!=null)
            {
                foreach(var tag in tags)
                {
                    string name, value;
                    TagHandle.GetPropertyNameAndValue(tag,out name,out value);
                    IdentifyValue(rs, name, value);
                }
            }
            return rs;
        }
        void IdentifyValue(Content content,string name,string value)
        {
            switch (name)
            {
                case "Speaker":
                    content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                default:
                    Debug.LogError("无法识别的标签类型："+name+":"+value);
                    return;
            }
        }
        void IdentifyValue(Choice choice, string name, string value)
        {
            switch (name)
            {
                case "Speaker":
                    choice.content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                default:
                    Debug.LogError("无法识别的标签类型：" + name + ":" + value);
                    return;
            }
        }
        /// <summary>
        /// 获取当前所有的选项
        /// </summary>
        /// <returns>选项的集合</returns>
        public List<Choice> CurrentChoices()
        {
            var choices = story.currentChoices;
            foreach(var choice in choices)
            {

            }
            return rs;
        }
        /// <summary>
        /// 根据索引值来进行选择
        /// </summary>
        /// <param name="index">当前选项的索引值</param>
        public void SelectChoice(int index)
        {
            story.ChooseChoiceIndex(index);
        }
        /// <summary>
        /// 根据现选项来进行选择
        /// </summary>
        /// <param name="choice">当前选项</param>
        public void SelectChoice(Choice choice)
        {
            story.ChooseChoiceIndex(choice.index);
        }
        public void CreateStory()
        {

        }
    }

}
