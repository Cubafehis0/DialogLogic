using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

/// <summary>
/// �����࣬��װ��һЩInk�ṩ��API
/// </summary>
namespace Ink2Unity
{
    /// <summary>
    /// ����Ink��TextAsset������һ��Ink2Unityʵ��
    /// </summary>
    public class Ink2Unity
    {
        Story story;
        Player player;
        List<Choice> choicesList;
        public Ink2Unity(TextAsset inkJSON)
        {
            story = new Story(inkJSON.text);
            player = CardGameManager.Instance.player;
            story.BindExternalFunction("InsidePlus", (int a) => { player.Inside += a; });
            story.BindExternalFunction("OutsidePlus", (int a) => { player.Outside += a; });
            story.BindExternalFunction("LogicPlus", (int a) => { player.Logic += a; });
            story.BindExternalFunction("PassionPlus", (int a) => { player.Passion += a; });
            story.BindExternalFunction("MoralPlus", (int a) => { player.Moral += a; });
            story.BindExternalFunction("UnethicPlus", (int a) => { player.Unethic += a; });
            story.BindExternalFunction("DetourPlus", (int a) => { player.Detour += a; });
            story.BindExternalFunction("StrongPlus", (int a) => { player.Strong += a; });

            story.BindExternalFunction("InsideSet", (int a) => { player.Inside = a; });
            story.BindExternalFunction("OutsideSet", (int a) => { player.Outside = a; });
            story.BindExternalFunction("LogicSet", (int a) => { player.Logic = a; });
            story.BindExternalFunction("PassionSet", (int a) => { player.Passion = a; });
            story.BindExternalFunction("MoralSet", (int a) => { player.Moral = a; });
            story.BindExternalFunction("UnethicSet", (int a) => { player.Unethic = a; });
            story.BindExternalFunction("DetourSet", (int a) => { player.Detour = a; });
            story.BindExternalFunction("StrongSet", (int a) => { player.Strong = a; });

            story.BindExternalFunction("InsideIsIn", (int l, int r) => { return player.Inside >= l && player.Inside < r; });
            story.BindExternalFunction("OutsideIsIn", (int l, int r) => { return player.Outside >= l && player.Outside < r; });
            story.BindExternalFunction("LogicIsIn", (int l, int r) => { return player.Logic >= l && player.Logic < r; });
            story.BindExternalFunction("PassionIsIn", (int l, int r) => { return player.Passion >= l && player.Passion < r; });
            story.BindExternalFunction("MoralIsIn", (int l, int r) => { return player.Moral >= l && player.Moral < r; });
            story.BindExternalFunction("UnthicIsIn", (int l, int r) => { return player.Unethic >= l && player.Unethic < r; });
            story.BindExternalFunction("DetourIsIn", (int l, int r) => { return player.Detour >= l && player.Detour < r; });
            story.BindExternalFunction("StrongIsIn", (int l, int r) => { return player.Strong>= l && player.Strong < r; });

            Ink.Runtime.Story.ExternalFunction choiceCanUse = (object[] args) =>
              {
                  return ChoiceCanUse((int)args[0], (int)args[1], (int)args[2], (int)args[3]);
              };
            Ink.Runtime.Story.ExternalFunction judge = (object[] args) =>
            {
                return TalkJudge((int)args[0], (int)args[1], (int)args[2], (int)args[3]);
            };
            story.BindExternalFunctionGeneral("TalkJudge",judge);
            story.BindExternalFunctionGeneral("ChoiceCanUse",choiceCanUse);
        }
        /// <summary>
        /// �����Ƿ�ɼ�����ȡ����(Content)
        /// </summary>
        public bool CanCoutinue
        {
            get
            {
                return story.canContinue;
            }
        }
        /// <summary>
        /// ��ҽ��в��������ƣ�ѡ�񣩺������øú����Ը���Ink��״̬
        /// </summary>
        public void UpdateInk()
        {
            UpdateInkVariable();
        }
        /// <summary>
        ///     
        /// </summary>
        public Content CurrentContent()
        {
            string ct;
            //������ʽ�����
            while ((ct = story.Continue()) == "") ;
            UpdateInkVariable();
            Content rs = new Content(ct);
            List<string> tags = story.currentTags;
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    string name, value;
                    TagHandle.GetPropertyNameAndValue(tag, out name, out value);
                    ParseValue(rs, name, value);
                }
            }
            return rs;
        }
        public bool IsFinished
        {
            get
            {
                return !story.canContinue && story.currentChoices.Count == 0;
            }
        }
        /// <summary>
        /// ��ȡ��ǰ���е�ѡ��
        /// </summary>
        /// <returns>ѡ��ļ���</returns>
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
        /// ��������ֵ������ѡ�񣬻᷵��ѡ��ѡ�����ֵ��ı�
        /// </summary>
        /// <param name="index">��ǰѡ�������ֵ</param>
        public Content SelectChoice(int index)
        {
            story.ChooseChoiceIndex(index);
            UpdateInkVariable();
            ///
            // �����ж��Ĺ���
            ///
            Content c = CurrentContent();
            string t;
            TagHandle.ChoiceCurrentTags(c.richText, out t);
            if (t != "")
            {
                c.speaker = Speaker.Lead;
                c.richText = t;
                return c;
            }
            return null;

        }
        /// <summary>
        /// ������ѡ��������ѡ�񣬻᷵��ѡ��ѡ�����ֵ��ı�
        /// </summary>
        /// <param name="choice">��ǰѡ��</param>
        public Content SelectChoice(Choice choice)
        {
            return SelectChoice(choice.index);
        }


        private bool  ChoiceCanUse(int i, int l, int m, int d)
        {
            int[] p = { i, l, m, d };
            for (int j = 0; j < 4; j++)
            {
                if (p[j] > 0)
                {
                    if (player.data[j]< p[j])
                        return false;
                }
                else if (p[j] < 0)
                {
                    if (player.data[j]> p[j])
                        return false;
                }
            }
            return true;
        }
        private bool TalkJudge(int i,int l,int m,int d)
        {
            int r = GetRandomJudge();
            int[] p = { i, l, m, d };
            for(int j=0;j<4;j++)
            {
                if(p[j]>0)
                {
                    if (player.data[j] + r < p[j])
                        return false;
                }
                else if(p[j]<0)
                {
                    if (player.data[j] - r > p[j])
                        return false;
                }
            }
            return true;
        }
        //��ͬ�ж������ĸ���
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
        private void UpdateInkVariable()
        {
            story.variablesState["Inside"] = player.Inside;
            story.variablesState["Outside"] = player.Outside;
            story.variablesState["Logic"] = player.Logic;
            story.variablesState["Passion"] = player.Passion;
            story.variablesState["Moral"] = player.Moral;
            story.variablesState["Unethic"] = player.Unethic;
            story.variablesState["Detour"] = player.Detour;
            story.variablesState["Strong"] = player.Strong;
        }
      
       
        void ParseValue(Content content,string name,string value)
        {
            switch (name)
            {
                case "Speaker":
                    content.speaker = TagHandle.ParseSpeaker(value);
                    return;
                default:
                    Debug.LogError("�޷�ʶ��ı�ǩ���ͣ�"+name+":"+value);
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
                    choice.canUse = TagHandle.ParseBool(value);
                    return;
                case "SpeechArt":
                    choice.speechArt = TagHandle.ParseSpeechArt(value);
                    return;
                default:
                    Debug.LogError("�޷�ʶ��ı�ǩ���ͣ�" + name + ":" + value);
                    return;
            }
        }
    }

}
