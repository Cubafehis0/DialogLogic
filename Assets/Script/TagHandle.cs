using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public abstract class TagHandle
{
    static string tagParttern = @"^([^:]+)(:)([^:]+)";
    static string choiceTagParttern = @"([^@]+)(@)(.+)";

    static string conditionParttern;
    static string stateParrern;
    public static bool GetPropertyNameAndValue(string str,out string name,out string content)
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
    public static string GetPureText(string richText)
    {
       
        return null;
    }
    public static List<string> ChoiceCurrentTags(string choiceContent,out string choice)
    {
        var match= Regex.Match(choiceContent, choiceTagParttern);
        if(match.Success)
        {
            List<string> result = new List<string>();
            string tags = match.Groups[3].Value.Trim();
            string tp = @"\S+";
            var matches = Regex.Matches(tags,tp);
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
        switch(speaker)
        {
            case "����":
                return Speaker.Lead;
            case "NPC":
                return Speaker.NPC;
            case "�԰�":
                return Speaker.Dialogue;
            default:
                Debug.LogError("�޷�ʶ���Speaker��ǩ");
                return Speaker.Dialogue;
        }
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
