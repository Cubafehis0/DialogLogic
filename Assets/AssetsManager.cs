using ModdingAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SpeechTypeSpriteDictionary : SerializableDictionary<SpeechType, Sprite> { }

[Serializable]
public class PersonalityTypeSpriteDictionary : SerializableDictionary<PersonalityType, Sprite> { }
public class AssetsManager : Singleton<AssetsManager>
{
    public SpeechTypeSpriteDictionary choiceSprites;
    public PersonalityTypeSpriteDictionary conditionIcons;
    public StringSpriteDictionary enemySpriteDictionary;
}
