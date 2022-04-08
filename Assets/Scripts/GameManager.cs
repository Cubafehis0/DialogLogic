using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using System.Xml;
using System.Reflection;
using System;
using System.Xml.Serialization;
using System.IO;
using SemanticTree.PlayerEffect;
using SemanticTree;
using UnityEditor;
[Serializable]
public class SpeechTypeSpriteDictionary : SerializableDictionary<SpeechType, Sprite> { }

[Serializable]
public class PersonalityTypeSpriteDictionary : SerializableDictionary<PersonalityType, Sprite> { }

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpeechTypeSpriteDictionary choiceSprites;
    [SerializeField]
    private Sprite conditonCover;
    [SerializeField]
    private PersonalityTypeSpriteDictionary conditionIcons;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public PersonalityTypeSpriteDictionary ConditionIcons { get => conditionIcons; set => conditionIcons = value; }
    public SpeechTypeSpriteDictionary ChoiceSprites { get => choiceSprites; set => choiceSprites = value; }
    public Sprite ConditonCover { get => conditonCover; set => conditonCover = value; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ConfigRead.LoadAll();
        CardGameManager.Instance.StartGame();
    }
    

    public static T Deserialize<T>(string s)
    {

        XmlSerializer ser = new XmlSerializer(typeof(T));
        using var stream = StreamString2Stream(s);
        return (T)ser.Deserialize(stream);
    }

    public static T Deserialize<T>(FileStream fs)
    {

        XmlSerializer ser = new XmlSerializer(typeof(T));
        return (T)ser.Deserialize(fs);
    }

    private static Stream StreamString2Stream(string s)
    {
        MemoryStream stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    private void Update()
    {
        instance = this;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}