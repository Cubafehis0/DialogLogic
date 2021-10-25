using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace Ink2Unity
{
    public abstract class TagHandle
    {
        static string tagParttern = @"^([^:]+)(:)([^:]+)";
        static string choiceTagParttern = @"([^@]*)(@)(.+)";

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
                    Debug.LogError(str + "格式错误");
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
                Debug.LogError(str + "格式错误");
                name = null;
                content = null;
                return false;
            }
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
                string tags = match.Groups[3].Value.Trim();
                string tp = @"\S+";
                var matches = Regex.Matches(tags, tp);
                foreach (Match m in matches)
                {
                    result.Add(m.Value);
                    //Debug.Log(m.Value);
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
                case "主角":
                    return Speaker.Lead;
                case "NPC":
                    return Speaker.NPC;
                case "旁白":
                    return Speaker.Dialogue;
                default:
                    Debug.LogError("无法识别的Speaker标签");
                    return Speaker.Dialogue;
            }
        }
        public static SpeechArt ParseSpeechArt(string sa)
        {
            switch (sa)
            {
                case "说服":
                   return SpeechArt.Persuade;
                case "欺骗":
                    return SpeechArt.Cheat;
                case "威胁":
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
            Debug.LogError(s+"无法识别的布尔型");
            return true;
        }
        //翻译Condition生成一个Lambda表达式
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
