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
    public class InkStory
    {
        private static InkStory _nowStory;
        public static InkStory NowStory
        {
            get
            {
                return _nowStory;
            }
            private set
            {
                _nowStory = value;
            }
        }
        Story story;
        Player player;
        List<Choice> choicesList;
        public InkStory(TextAsset inkJSON)
        {
            story = new Story(inkJSON.text);
            //player = CardGameManager.Instance.player;
            player = new Player();
            BindExternalFunction();
            NowStory = this;
        }
        public void BindExternalFunction()
        {
            Ink.Runtime.Story.ExternalFunction stateSet = (object[] args) =>
              {
                  StateSet((int)args[0], (int)args[1], (int)args[2], (int)args[3]);
                  return null;
              };
            Ink.Runtime.Story.ExternalFunction stateChange= (object[] args) =>
            {
                StateChange((int)args[0], (int)args[1], (int)args[2], (int)args[3], (int)args[4]);
                return null;
            };
            story.BindExternalFunction("InnIsIn", (int l, int r) => { return player.Inside >= l && player.Inside < r; });
            story.BindExternalFunction("ExtIsIn", (int l, int r) => { return player.Outside >= l && player.Outside < r; });
            story.BindExternalFunction("LgcIsIn", (int l, int r) => { return player.Logic >= l && player.Logic < r; });
            story.BindExternalFunction("SptIsIn", (int l, int r) => { return player.Passion >= l && player.Passion < r; });
            story.BindExternalFunction("MrlIsIn", (int l, int r) => { return player.Moral >= l && player.Moral < r; });
            story.BindExternalFunction("UtcIsIn", (int l, int r) => { return player.Unethic >= l && player.Unethic < r; });
            story.BindExternalFunction("RdbIsIn", (int l, int r) => { return player.Detour >= l && player.Detour < r; });
            story.BindExternalFunction("AgsIsIn", (int l, int r) => { return player.Strong >= l && player.Strong < r; });
            Ink.Runtime.Story.ExternalFunction choiceCanUse = (object[] args) =>
            {
                return ChoiceCanUse((int)args[0], (int)args[1], (int)args[2], (int)args[3]);
            };
            Ink.Runtime.Story.ExternalFunction judge = (object[] args) =>
            {
                return TalkJudge((int)args[0], (int)args[1], (int)args[2], (int)args[3]);
            };
            story.BindExternalFunctionGeneral("TalkJudge", judge);
            story.BindExternalFunctionGeneral("ChoiceCanUse", choiceCanUse);
            story.BindExternalFunctionGeneral("StateSet", stateSet);
            story.BindExternalFunctionGeneral("StateChange", stateChange);
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
        ///   ��ȡ��ǰ����  
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
        /// ��������ֵ������ѡ�񣬻᷵��ѡ��ѡ�����ֵ��ı������û�ж�Ӧ�ı����򷵻ؿ�
        /// </summary>
        /// <param name="index">��ǰѡ�������ֵ</param>
        public Content SelectChoice(int index)
        {
            story.ChooseChoiceIndex(index);
            UpdateInkVariable();
            ///
            // �����ж��Ĺ���
            ///����ԭ������
            Content c = CurrentContent();
            //���ö����������
            c = CurrentContent();
            if (c.richText != "NIL")
                return c;
            return null;

        }
        /// <summary>
        /// ������ѡ��������ѡ�񣬻᷵��ѡ��ѡ�����ֵ��ı������û���ı�������Ϊ��
        /// </summary>
        /// <param name="choice">��ǰѡ��</param>
        public Content SelectChoice(Choice choice)
        {
            return SelectChoice(choice.index);
        }
        /// <summary>
        /// ��ȡ��ǰѡ�����
        /// </summary>
        public int CurrentChoicesCount
        {
            get
            {
                return CurrentChoices().Count;
            }
        }
        private void StateChange(int i,int l,int m,int r,int t)
        {
            int[] p = { i, l, m, r };
            for(int j=0;j<4;j++)
            {
                player.data[j] += p[j];
            }
            if(t!=-1)
            {
                //�ӳ�buff

            }
        }

        private void StateSet(int i, int l, int m, int r)
        {
            int[] p={ i,l,m,r};
            for(int j=0;j<4;j++)
                player.data[j] = p[j];
        }
        public string NowState2Json()
        {
            return story.state.ToJson();
        }
        public void LoadStory(string state)
        {
            story.state.LoadJson(state);
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
            story.variablesState["inn"] = player.Inside;
            story.variablesState["ext"] = player.Outside;
            story.variablesState["lgc"] = player.Logic;
            story.variablesState["spt"] = player.Passion;
            story.variablesState["mrl"] = player.Moral;
            story.variablesState["utc"] = player.Unethic;
            story.variablesState["rdb"] = player.Detour;
            story.variablesState["ags"] = player.Strong;
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
