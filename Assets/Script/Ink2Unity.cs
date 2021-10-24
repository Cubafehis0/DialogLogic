using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
namespace zc
{
    /// <summary>
    /// �����࣬��װ��һЩInk�ṩ��API
    /// </summary>
    public class Ink2Unity
    {
        Story story;
        public Ink2Unity(TextAsset inkJSON)
        {
            story = new Story(inkJSON.text);
        }
        /// <summary>
        /// �����Ƿ�ɼ�����ȡ����(Content)
        /// </summary>
        public bool CanCoutinue()
        {
            return story.canContinue;
        }
        /// <summary>
        /// ��ǰ���µ�����
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
                    Debug.LogError("�޷�ʶ��ı�ǩ���ͣ�"+name+":"+value);
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
                    Debug.LogError("�޷�ʶ��ı�ǩ���ͣ�" + name + ":" + value);
                    return;
            }
        }
        /// <summary>
        /// ��ȡ��ǰ���е�ѡ��
        /// </summary>
        /// <returns>ѡ��ļ���</returns>
        public List<Choice> CurrentChoices()
        {
            var choices = story.currentChoices;
            foreach(var choice in choices)
            {

            }
            return rs;
        }
        /// <summary>
        /// ��������ֵ������ѡ��
        /// </summary>
        /// <param name="index">��ǰѡ�������ֵ</param>
        public void SelectChoice(int index)
        {
            story.ChooseChoiceIndex(index);
        }
        /// <summary>
        /// ������ѡ��������ѡ��
        /// </summary>
        /// <param name="choice">��ǰѡ��</param>
        public void SelectChoice(Choice choice)
        {
            story.ChooseChoiceIndex(choice.index);
        }
        public void CreateStory()
        {

        }
    }

}
