using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace Ink2Unity
{
    public abstract class TagHandle
    {
        static string tagParttern = @"^([^:]+)(:)([^:]+)";
        static string choiceTagParttern = @"([^@]*)(@.+)";

        static string conditionParttern;
        static string stateParrern;
        public static bool GetPropertyNameAndValue(string str, out string name, out string content)
        {
            var match = Regex.Match(str, tagParttern);
            if (match.Success)
            {
                var group = match.Groups;
                if (group.Count != 4)
                {
                    Debug.LogError(str + "��ʽ����");
                    name = null;
                    content = null;
                    return false;
                }
                name = group[1].Value.Trim();
                content = group[3].Value.Trim();
                return true;
            }
            else
            {
                Debug.LogError(str + "��ʽ����");
                name = null;
                content = null;
                return false;
            }
        }
        public static List<int> ParseArray(string value)
        {
            string parttern = @"([^\d]+)(\d+)";
            var matches = Regex.Matches(value,parttern);
            if(matches.Count>0)
            {
                List<int> res = new List<int>();
                foreach (Match m in matches)
                {
                    res.Add(int.Parse(m.Groups[2].Value));
                }
                return res;
            }
            return null;
        }
        public static string GetPureText(string richText)
        {

            return null;
        }
        public static List<string> ChoiceCurrentTags(string choiceContent, out string choice)
        {
            var match = Regex.Match(choiceContent, choiceTagParttern);
            if (match.Success)
            {
                List<string> result = new List<string>();
                string tags = match.Groups[2].Value.Trim();
                string tp = @"(@)(\S+)(\s*)";
                var matches = Regex.Matches(tags, tp);
                foreach (Match m in matches)
                {
                    result.Add(m.Groups[2].Value);
                }
                choice = match.Groups[1].Value.Trim();
                return result;
            }
            choice = choiceContent;
            return null;
        }
        //
        public static Speaker ParseSpeaker(string speaker)
        {
            switch (speaker)
            {
                case "Player":
                    return Speaker.Player;
                case "NPC":
                    return Speaker.NPC;
                case "Dialogue":
                    return Speaker.Dialogue;
                default:
                    Debug.LogError("�޷�ʶ���Speaker��ǩ");
                    return Speaker.Dialogue;
            }
        }
        public static SpeechArt ParseSpeechArt(string sa)
        {
            switch (sa)
            {
                case "Persuade":
                   return SpeechArt.Persuade;
                case "Cheat":
                    return SpeechArt.Cheat;
                case "Threaten":
                    return SpeechArt.Threaten;
                default:
                    return SpeechArt.Normal;
            }
            
        }
        public static bool ParseBool(string s)
        {
            if(s=="true")
                return true;
            if (s == "false")
                return false;
            Debug.LogError(s+"�޷�ʶ��Ĳ�����");
            return true;
        }
        //����Condition����һ��Lambda���ʽ
        public static bool ParseCondition()
        {
            return true;
        }
        public static bool ParseStateChange()
        {
            return true;
        }

    }

}
