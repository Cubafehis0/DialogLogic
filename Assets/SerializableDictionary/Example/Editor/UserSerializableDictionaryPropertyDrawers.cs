using UnityEditor;

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
[CustomPropertyDrawer(typeof(SpeechTypeSpriteDictionary))]
[CustomPropertyDrawer(typeof(PersonalityTypeSpriteDictionary))]
[CustomPropertyDrawer(typeof(SpeakerGameObjectDictionary))]
[CustomPropertyDrawer(typeof(MapStateSpriteDictionary))]
[CustomPropertyDrawer(typeof(StringSpriteDictionary))]
[CustomPropertyDrawer(typeof(StringWindowDictionary))]
[CustomPropertyDrawer(typeof(StringPawnDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }
