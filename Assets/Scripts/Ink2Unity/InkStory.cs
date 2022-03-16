using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Events;

/// <summary>
/// 工具类，封装了一些Ink提供的API
/// </summary>
namespace Ink2Unity
{
    public enum InkState
    {
        Init,
        Content,
        Choice,
        Finish
    }

    public interface IPlayerStateChange
    {
        void StateChange(Personality delta, int duration);
    }

    public interface IInkStory
    {
        InkState NextState { get; }
        List<Choice> CurrentChoices();
        Content CurrentContent();
        Content NextContent();
        Content SelectChoice(Choice choice, bool success);
        Content SelectChoice(int index, bool success);
    }

    public interface ISavable
    {
        void LoadStory(string state);
        string NowState2Json();
    }


    /// <summary>
    /// 传入Ink的TextAsset来创建一个Ink2Unity实例
    /// </summary>
    public class InkStory : IInkStory,ISavable,ISaveAndLoad
    {
        private static InkStory _nowStory;
        public static InkStory NowStory { get => _nowStory; }

        public InkState NextState
        {
            get
            {
                if (story.canContinue)
                    return InkState.Content;
                if (story.currentChoices.Count > 0)
                    return InkState.Choice;
                return InkState.Finish;
            }
        }

        private Story story;
        private List<Choice> choicesList;


        public InkStory(TextAsset inkJSON)
        {
            story = new Story(inkJSON.text);
            choicesList = null;
            _nowStory = this;
            BindExternalFunction();
        }

        /// <summary>
        /// 只有下面是Content时生效，否则返回null
        /// </summary>
        /// <returns></returns>
        public Content NextContent()
        {
            if (NextState != InkState.Content)
                return null;
            //避免表达式的情况
            while (story.Continue() == "")
                if (NextState != InkState.Content)
                    return null;
            return CurrentContent();
        }

        /// <summary>
        ///   获取当前内容  
        /// </summary>
        public Content CurrentContent()
        {
            string ct = story.currentText;
            Content rs = new Content(ct);
            List<string> tags = story.currentTags;
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    TagHandle.GetPropertyNameAndValue(tag, out string name, out string value);
                    ParseValue(rs, name, value);
                }
            }
            CardPlayerState.Instance.StateChange(rs.personalityModifier, rs.changeTurn);
            return rs;
        }

        /// <summary>
        /// 获取当前所有的选项
        /// </summary>
        /// <returns>选项的集合</returns>
        public List<Choice> CurrentChoices()
        {
            List<Choice> rs = new List<Choice>();
            var choicesContent = story.currentChoices;
            for (int i = 0; i < choicesContent.Count; i++)
            {
                string c;
                List<string> tags = TagHandle.ChoiceCurrentTags(choicesContent[i].text, out c);
                Choice choice = new Choice(new Content(c), choicesContent[i].index);
                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        string name, value;
                        TagHandle.GetPropertyNameAndValue(tag, out name, out value);
                        ParseValue(choice, name, value);
                    }
                }
                rs.Add(choice);
            }
            choicesList = rs;
            return rs;
        }


        public Content SelectChoice(int index, bool success)
        {
            Choice cs = CurrentChoices()[index];
            story.variablesState["judgeSuccess"] = success;
            CardPlayerState.Instance.Pressure += success ? -cs.Success_desc : cs.Fail_add;
            CardPlayerState.Instance.StateChange(cs.Content.personalityModifier, cs.Content.changeTurn);
            story.ChooseChoiceIndex(index);
            // 进行判定的过程
            // 忽略原本内容
            Content c = NextContent();
            return c.richText != "NIL" ? c : null;
        }
        /// <summary>
        /// 根据现选项来进行选择，会返回选择选项后出现的文本，如果没有文本返回则为空
        /// </summary>
        /// <param name="choice">当前选项</param>
        public Content SelectChoice(Choice choice, bool success)
        {
            return SelectChoice(choice.Index, success);
        }

        public string NowState2Json()
        {
            return story.state.ToJson();
        }
        public void LoadStory(string state)
        {
            story.state.LoadJson(state);
        }

        private void BindExternalFunction()
        {
            story.BindExternalFunction("InnIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Inside, l, r));
            story.BindExternalFunction("ExtIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Outside, l, r));
            story.BindExternalFunction("LgcIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Logic, l, r));
            story.BindExternalFunction("SptIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Passion, l, r));
            story.BindExternalFunction("MrlIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Moral, l, r));
            story.BindExternalFunction("UtcIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Unethic, l, r));
            story.BindExternalFunction("RdbIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Detour, l, r));
            story.BindExternalFunction("AgsIsIn", (int l, int r) => InBound(CardPlayerState.Instance.FinalPersonality, PersonalityType.Strong, l, r));
        }

        private bool InBound(Personality personality, PersonalityType type, int l, int r)
        {
            return personality[type] >= l && personality[type] < r;
        }

        public void BindPlayerInfo(UnityEvent uevent)
        {
            uevent.AddListener(UpdateInkPlayerInfo);
        }

        private void UpdateInkPlayerInfo()
        {
            Personality personality = CardPlayerState.Instance.FinalPersonality;
            story.variablesState["inn"] = personality.Inside;
            story.variablesState["ext"] = personality.Outside;
            story.variablesState["lgc"] = personality.Logic;
            story.variablesState["spt"] = personality.Passion;
            story.variablesState["mrl"] = personality.Moral;
            story.variablesState["utc"] = personality.Unethic;
            story.variablesState["rdb"] = personality.Detour;
            story.variablesState["ags"] = personality.Strong;
        }

        private void ParseValue(Content content, string name, string value)
        {
            switch (name)
            {
                case "Speaker":
                    content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                case "StateChange":
                    List<int> a = TagHandle.ParseArray(value);
                    content.personalityModifier = new Personality(a.GetRange(0, 4));
                    content.changeTurn = a[4];
                    return;
                default:
                    Debug.LogError("无法识别的标签类型：" + name + ":" + value);
                    return;
            }
        }
        private void ParseValue(Choice choice, string name, string value)
        {
            switch (name)
            {
                case "Speaker":
                    choice.Content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                case "CanUse":
                    List<int> values = TagHandle.ParseArray(value);
                    choice.JudgeValue = new Personality(values);
                    return;
                case "SpeechArt":
                    choice.SpeechArt = TagHandle.ParseSpeechArt(value);
                    return;
                case "Success":
                    choice.Success_desc = int.Parse(value);
                    return;
                case "Fail":
                    choice.Fail_add = int.Parse(value);
                    return;
                case "StateChange":
                    List<int> a = TagHandle.ParseArray(value);
                    choice.Content.personalityModifier = new Personality(a.GetRange(0, 4));
                    choice.Content.changeTurn = a[4];
                    return;
                default:
                    Debug.LogError("无法识别的标签类型：" + name + ":" + value);
                    return;
            }
        }

        public void Register()
        {
            SaveAndLoad.OnLoad.AddListener(Load);
            SaveAndLoad.OnSave.AddListener(Save);
        }

        public void Save(SaveInfo saveInfo)
        {
            saveInfo.AddSaveInfo(typeof(InkStory), story.state.ToJson());
        }

        public void Load(SaveInfo saveInfo)
        {
            string state = saveInfo.GetSaveInfo(typeof(InkStory)) as string;
            if(state==null)
            {
                Debug.LogError("存档信息有误，无法读取InkStory的存档信息");
                return;
            }
            story.state.LoadJson(state);
        }
    }

}
