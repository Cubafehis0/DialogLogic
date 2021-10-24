using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
namespace Ink2Unity
{
    /// <summary>
    /// 工具类，封装了一些Ink提供的API
    /// </summary>
    public class Ink2Unity
    {
        Story story;
        Player player;
        List<Choice> choices;
        public Ink2Unity(TextAsset inkJSON)
        {
            story = new Story(inkJSON.text);
            player = CardGameManager.Instance.player;
            story.BindExternalFunction("logicPlus", (int a) => { player.Ration += a; });
            story.BindExternalFunction("moralPlus", (int a) => { player.Moral += a; });
            story.BindExternalFunction("strongPlus", (int a) => { player.Strong += a; });
            story.BindExternalFunction("choiceCanUse", (int i, int l, int m, int d) =>
            { return player.Inside >= i && player.Ration >= l && player.Moral >= m && player.Detour >= d; });
        }
        /// <summary>
        /// 故事是否可继续读取内容(Content)
        /// </summary>
        public bool CanCoutinue
        {
            get
            {
                return story.canContinue;
            }
        }
        //public void Continue()
        //{
        //    if (CanCoutinue)
        //        story.Continue();
        //    else
        //        Debug.LogError("当前故事");
        //}
        /// <summary>
        /// 当前故事的内容
        /// </summary>
        public Content CurrentContent()
        {
            string ct;
            //避免表达式的情况
            while((ct = story.Continue())=="");

            Content rs = new Content(ct);
            List<string> tags = story.currentTags;
            if(tags!=null)
            {
                foreach(var tag in tags)
                {
                    string name, value;
                    TagHandle.GetPropertyNameAndValue(tag,out name,out value);
                    ParseValue(rs, name, value);
                }
            }
            return rs;
        }
        void ParseValue(Content content,string name,string value)
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
        void ParseValue(Choice choice, string name, string value)
        {
            switch (name)
            {
                case "Speaker":
                    choice.content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                case "Canuse":
                    choice.CanUse = ;
                    return;
                
                    return;
                case "SpeechArt":
                    choice.speechArt = TagHandle.ParseSpeechArt(value);
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
            List<Choice> rs = new List<Choice>();
            var choicesContent = story.currentChoices;
            for(int i=0;i<choicesContent.Count;i++)
            {
                string c;
                List<string> tags = TagHandle.ChoiceCurrentTags(choicesContent[i].text,out c);
                Choice choice = new Choice();
                choice.content = new Content(c);
                foreach(var tag in tags)
                {
                    string name, value;
                    TagHandle.GetPropertyNameAndValue(tag, out name, out value);
                    ParseValue(choice, name, value);
                }
                rs.Add(choice);
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
