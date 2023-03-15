using Ink.Runtime;
using ModdingAPI;
using System.Collections.Generic;
using UnityEngine;
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

    public interface ISavable
    {
        void LoadStory(string state);
        string NowState2Json();
    }

    public interface IReadOnlyStory
    {
        InkState NextState { get; }
        List<Choice> CurrentChoices { get; }
        Content CurrentContent { get; }
    }

    public interface IInkStory
    {
        InkState NextState { get; }
        List<Choice> CurrentChoices { get; }
        Content CurrentContent { get; }
        void BindPlayerInfo(UnityEvent uevent);
        void Load(SaveInfo saveInfo);
        void LoadStory(string state);
        Content NextContent();
        string NowState2Json();
        void Save(SaveInfo saveInfo);
        Content SelectChoice(Choice choice, bool success);
        Content SelectChoice(int index, bool success);
    }


    /// <summary>
    /// 传入Ink的TextAsset来创建一个Ink2Unity实例
    /// </summary>
    public class InkStory : ISavable, ISaveAndLoad, IInkStory, IReadOnlyStory
    {
        private Story story;
        private Content currentContent;
        private List<Choice> currentChoices;


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

        public InkStory(TextAsset inkJSON)
        {
            story = new Story(inkJSON.text);
            BindExternalFunction();
            UpdateCurrentChoices();
            UpdateCurrentContent();
        }

        /// <summary>
        /// 获取当前所有的选项
        /// </summary>
        /// <returns>选项的集合</returns>
        public List<Choice> CurrentChoices => currentChoices;

        /// <summary>
        ///  获取当前内容  
        /// </summary>
        public Content CurrentContent => currentContent;

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
            UpdateCurrentChoices();
            UpdateCurrentContent();

            Content rs = CurrentContent;
            if (rs.personalityModifier != null)
                GameConsole.Instance.ModifyPersonality("", rs.personalityModifier, rs.changeTurn, DMGType.Normal);
            return CurrentContent;
        }

        private void UpdateCurrentContent()
        {
            currentContent = new Content(story.currentText ?? "Empty");
            story.currentTags?.ForEach(tag =>
            {
                TagHandle.GetPropertyNameAndValue(tag, out string name, out string value);
                currentContent.SetValue(name, value);
            });
        }


        private void UpdateCurrentChoices()
        {
            currentChoices = story.currentChoices?.ConvertAll(inkChoice =>
            {
                List<string> tags = TagHandle.ChoiceCurrentTags(inkChoice.text, out string c);
                Choice choice = new Choice(new Content(c), inkChoice.index);
                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        TagHandle.GetPropertyNameAndValue(tag, out string name, out string value);
                        choice.SetValue(name, value);
                    }
                }
                return choice;
            });
        }




        public Content SelectChoice(int index, bool success)
        {
            Choice cs = CurrentChoices[index];
            story.variablesState["judgeSuccess"] = success;
            GameConsole.Instance.AddPressure("", success ? -cs.Success_desc : cs.Fail_add);
            if (cs.Content.personalityModifier != null)
                GameConsole.Instance.ModifyPersonality("", cs.Content.personalityModifier, cs.Content.changeTurn, DMGType.Normal);
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
        public void LoadStory(string json)
        {
            story.state.LoadJson(json);
        }

        private void BindExternalFunction()
        {
            story.BindExternalFunction("InnIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Inside, l, r));
            story.BindExternalFunction("ExtIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Outside, l, r));
            story.BindExternalFunction("LgcIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Logic, l, r));
            story.BindExternalFunction("SptIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Passion, l, r));
            story.BindExternalFunction("MrlIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Moral, l, r));
            story.BindExternalFunction("UtcIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Unethic, l, r));
            story.BindExternalFunction("RdbIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Detour, l, r));
            story.BindExternalFunction("AgsIsIn", (int l, int r) => InBound(CardGameManager.Instance.playerState.GetFinalPersonality(), PersonalityType.Strong, l, r));
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
            Personality personality = CardGameManager.Instance.playerState.GetFinalPersonality();
            story.variablesState["inn"] = personality.Inner;
            story.variablesState["ext"] = personality.Outside;
            story.variablesState["lgc"] = personality.Logic;
            story.variablesState["spt"] = personality.Spiritial;
            story.variablesState["mrl"] = personality.Moral;
            story.variablesState["utc"] = personality.Immoral;
            story.variablesState["rdb"] = personality.Roundabout;
            story.variablesState["ags"] = personality.Aggressive;
        }
        public void Save(SaveInfo saveInfo)
        {
            saveInfo.AddSaveInfo(typeof(InkStory), story.state.ToJson());
        }

        public void Load(SaveInfo saveInfo)
        {
            string state = saveInfo.GetSaveInfo(typeof(InkStory)) as string;
            if (state == null)
            {
                Debug.LogError("存档信息有误，无法读取InkStory的存档信息");
                return;
            }
            story.state.LoadJson(state);
        }
    }

}
