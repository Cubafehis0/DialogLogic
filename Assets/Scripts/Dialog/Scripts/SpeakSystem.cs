using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class SpeakerGameObjectDictionary : SerializableDictionary<Speaker, GameObject> { }

public class SpeakSystem : MonoBehaviour
{
    [SerializeField]
    private SpeakerGameObjectDictionary dictionary = new SpeakerGameObjectDictionary();

    public void Speak(string richText, Speaker speaker)
    {
        var speakObject = dictionary[speaker];
        foreach (var o in dictionary.Values)
        {
            o.SetActive(o == speakObject);
        }
        var c = speakObject.GetComponentInChildren<RichTypeButton>();
        if (c) c.TypeText(richText);
    }

    public void Speak(string richText, string speaker)
    {
        if(Enum.TryParse(speaker, out Speaker e))
        {
            Speak(richText, e);
        }
        else
        {
            throw new ArgumentException(speaker + "不是有效类型");
        }

    }
}
