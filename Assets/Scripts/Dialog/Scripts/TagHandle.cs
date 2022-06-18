using ModdingAPI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Ink2Unity
{
    public abstract class TagHandle
    {
        private static string tagParttern = @"^([^:]+)(:)([^:]+)";
        private static string choiceTagParttern = @"([^@]*)(@.+)";
        private static string conditionParttern;
        private static string stateParrern;
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
        public static List<int> ParseArray(string value)
        {
            string parttern = @"([^-\+\d]+)([\+-]?\d+)";
            var matches = Regex.Matches(value, parttern);
            if (matches.Count > 0)
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
        public static SpeakerEnum ParseSpeaker(string speaker)
        {
            switch (speaker)
            {
                case "Player":
                    return SpeakerEnum.Player;
                case "NPC":
                    return SpeakerEnum.NPC;
                case "Dialogue":
                    return SpeakerEnum.Dialogue;
                default:
                    Debug.LogError("无法识别的Speaker标签");
                    return SpeakerEnum.Dialogue;
            }
        }
        public static SpeechType ParseSpeechArt(string sa)
        {
            switch (sa)
            {
                case "Persuade":
                    return SpeechType.Persuade;
                case "Cheat":
                    return SpeechType.Cheat;
                case "Threaten":
                    return SpeechType.Threaten;
                default:
                    return SpeechType.Normal;
            }

        }
        public static bool ParseBool(string s)
        {
            if (s == "true")
                return true;
            if (s == "false")
                return false;
            Debug.LogError(s + "无法识别的布尔型");
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
