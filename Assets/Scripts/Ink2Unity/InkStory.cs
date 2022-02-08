using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

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
        void StateChange(List<int> delta, int turn);
        void PressureChange(int delta);
    }

    public interface IInkStory
    {
        InkState NextState { get; }
        List<Choice> CurrentChoices();
        Content CurrentContent();
        Content NextContent();
        Content SelectChoice(Choice choice);
        Content SelectChoice(int index);
    }

    public interface ISavable
    {
        void LoadStory(string state);
        string NowState2Json();
    }


    /// <summary>
    /// 传入Ink的TextAsset来创建一个Ink2Unity实例
    /// </summary>
    public class InkStory : IInkStory,ISavable
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
            if (rs.stateChange != null)
                CardPlayerState.Instance.StateChange(rs.stateChange, rs.changeTurn);
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
                Choice choice = new Choice();
                choice.content = new Content(c);
                choice.index = choicesContent[i].index;
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
        /// <summary>
        /// 根据索引值来进行选择，会返回选择选项后出现的文本，如果没有对应文本，则返回空
        /// </summary>
        /// <param name="index">当前选项的索引值</param>
        public Content SelectChoice(int index)
        {
            Choice cs = CurrentChoices()[index];
            if (cs.judgeValue != null)
            {
                bool success = TalkJudge(cs.judgeValue);
                UpdateInkVariableByName<bool>("judgeSuccess", success);
                if (success)
                {
                    if (cs.success_desc != 0)
                        CardPlayerState.Instance.PressureChange(-cs.success_desc);
                }
                else
                {
                    if (cs.fail_add != 0)
                        CardPlayerState.Instance.PressureChange(cs.fail_add);
                }
            }
            var sc = cs.content.stateChange;
            if (sc != null)
                CardPlayerState.Instance.StateChange(sc, cs.content.changeTurn);
            UpdateInk();
            story.ChooseChoiceIndex(index);
            ///
            // 进行判定的过程
            ///忽略原本内容
            Content c = NextContent();
            if (c.richText != "NIL")
                return c;
            return null;

        }
        /// <summary>
        /// 根据现选项来进行选择，会返回选择选项后出现的文本，如果没有文本返回则为空
        /// </summary>
        /// <param name="choice">当前选项</param>
        public Content SelectChoice(Choice choice)
        {
            return SelectChoice(choice.index);
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
            story.BindExternalFunction("InnIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Inside >= l && CardPlayerState.Instance.Character.Inside < r; });
            story.BindExternalFunction("ExtIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Outside >= l && CardPlayerState.Instance.Character.Outside < r; });
            story.BindExternalFunction("LgcIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Logic >= l && CardPlayerState.Instance.Character.Logic < r; });
            story.BindExternalFunction("SptIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Passion >= l && CardPlayerState.Instance.Character.Passion < r; });
            story.BindExternalFunction("MrlIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Moral >= l && CardPlayerState.Instance.Character.Moral < r; });
            story.BindExternalFunction("UtcIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Unethic >= l && CardPlayerState.Instance.Character.Unethic < r; });
            story.BindExternalFunction("RdbIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Detour >= l && CardPlayerState.Instance.Character.Detour < r; });
            story.BindExternalFunction("AgsIsIn", (int l, int r) => { return CardPlayerState.Instance.Character.Strong >= l && CardPlayerState.Instance.Character.Strong < r; });
        }

        /// <summary>
        /// 玩家进行操作（打牌，选择）后来调用该函数以更新Ink的状态
        /// </summary>
        private void UpdateInk()
        {
            UpdateInkPlayerInfo();
        }

        private void StateSet(int i, int l, int m, int r)
        {
            int[] p = { i, l, m, r };
            for (int j = 0; j < 4; j++)
                CardPlayerState.Instance.Character.Personality[j] = p[j];
        }
        private bool ChoiceCanUse(List<int> values)
        {
            for (int j = 0; j < 4; j++)
            {
                if (values[j] > 0)
                {
                    if (CardPlayerState.Instance.Character.Personality[j] < values[j])
                        return false;
                }
                else if (values[j] < 0)
                {
                    if (CardPlayerState.Instance.Character.Personality[j] > values[j])
                        return false;
                }
            }
            return true;
        }
        private bool TalkJudge(List<int> values)
        {
            int r = GetRandomJudge();
            for (int j = 0; j < 4; j++)
            {
                if (values[j] > 0)
                {
                    if (CardPlayerState.Instance.Character.Personality[j] + r < values[j])
                        return false;
                }
                else if (values[j] < 0)
                {
                    if (CardPlayerState.Instance.Character.Personality[j] - r > values[j])
                        return false;
                }
            }
            return true;
        }
        //不同判定补正的概率
        float[] jp = { 0.05f, 0.2f, 0.5f, 0.2f, 0.05f };
        int GetRandomJudge()
        {
            float a = Random.value;
            float l = 0, r = 0;
            for (int i = 0; i < jp.Length; i++)
            {
                l = r;
                r = r + jp[i];
                if (a >= l && a < r)
                    return i;
            }
            return 0;
        }
        private void UpdateInkPlayerInfo()
        {
            story.variablesState["inn"] = CardPlayerState.Instance.Character.Inside;
            story.variablesState["ext"] = CardPlayerState.Instance.Character.Outside;
            story.variablesState["lgc"] = CardPlayerState.Instance.Character.Logic;
            story.variablesState["spt"] = CardPlayerState.Instance.Character.Passion;
            story.variablesState["mrl"] = CardPlayerState.Instance.Character.Moral;
            story.variablesState["utc"] = CardPlayerState.Instance.Character.Unethic;
            story.variablesState["rdb"] = CardPlayerState.Instance.Character.Detour;
            story.variablesState["ags"] = CardPlayerState.Instance.Character.Strong;
        }
        private void UpdateInkVariableByName<T>(string name, T value)
        {
            story.variablesState[name] = value;
        }

        void ParseValue(Content content, string name, string value)
        {
            switch (name)
            {
                case "Speaker":
                    content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                case "StateChange":
                    List<int> a = TagHandle.ParseArray(value);
                    content.stateChange = a.GetRange(0, 4);
                    content.changeTurn = a[4];
                    return;
                default:
                    Debug.LogError("无法识别的标签类型：" + name + ":" + value);
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
                case "CanUse":
                    List<int> values = TagHandle.ParseArray(value);
                    choice.judgeValue = values;
                    choice.canUse = ChoiceCanUse(values);
                    return;
                case "SpeechArt":
                    choice.speechArt = TagHandle.ParseSpeechArt(value);
                    return;
                case "Success":
                    choice.success_desc = int.Parse(value);
                    return;
                case "Fail":
                    choice.fail_add = int.Parse(value);
                    return;
                case "StateChange":
                    List<int> a = TagHandle.ParseArray(value);
                    choice.content.stateChange = a.GetRange(0, 4);
                    choice.content.changeTurn = a[4];
                    return;
                default:
                    Debug.LogError("无法识别的标签类型：" + name + ":" + value);
                    return;
            }
        }
    }

}
