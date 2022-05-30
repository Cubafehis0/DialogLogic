using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Events;
[Serializable]
public class SaveInfo
{
    private Dictionary<Type, object> saveInfos;
    public SaveInfo()
    {
        saveInfos = new Dictionary<Type, object>();
    }
    public void AddSaveInfo(Type type, object o)
    {
        if (saveInfos.ContainsKey(type))
            saveInfos[type] = o;
        saveInfos.Add(type, o);
    }
    public object GetSaveInfo(Type type)
    {
        bool s = saveInfos.TryGetValue(type, out object res);
        if (s) return res;
        return null;
    }
    //public string inkState;
    //玩家状态
    //public Player player;

    //卡牌信息
    //public List<T> cardInHand;
    //public List<T> cardInDeck;
    //public List<T> cardInGrave;
}
public interface ISaveAndLoad
{
    void Save(SaveInfo saveInfo);
    void Load(SaveInfo saveInfo);
}
public abstract class SaveAndLoad
{
    public static UnityEvent<SaveInfo> OnSave { get; } = new UnityEvent<SaveInfo>();
    public static UnityEvent<SaveInfo> OnLoad { get; } = new UnityEvent<SaveInfo>();

    public static void Register(ISaveAndLoad sub)
    {
        OnSave.AddListener(sub.Save);
        OnLoad.AddListener(sub.Load);
    }
    public static void SaveGame(int index, string note = "")
    {
        if (note.Length > 20)
        {
            Debug.LogError("输入存档备份信息过长,存档失败");
            return;
        }
        SaveInfo saveInfo = new SaveInfo();
        string path = Application.persistentDataPath + "/gamesave" + index + ".sav";
        if (File.Exists(path))
            File.Delete(path);
        OnSave?.Invoke(saveInfo);
        FileStream file = File.Create(path);
        //序列化保存
        //BinaryFormatter bf = new BinaryFormatter();
        //bf.Serialize(file, saveInfo);
        //保存为JSON
        string save2Json = JsonUtility.ToJson(saveInfo);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(note);
        writer.Write(save2Json);
        Debug.Log(note);
        Debug.Log(save2Json);
        writer.Close();
        file.Close();
        Debug.Log("Game Saved In" + Application.persistentDataPath + "/gamesave" + index + ".sav");
    }
    //private static SaveInfo CreateSaveInfoObject()
    //{
    //    SaveInfo saveInfo = new SaveInfo();
    //    saveInfo.inkState = Ink2Unity.InkStory.NowStory.NowState2Json();
    //    //saveInfo.player=
    //    //保存卡牌信息
    //    return saveInfo;
    //}

    public static void Load(int index)
    {
        //FileStream file = File.Open(Application.persistentDataPath + "/"+fileName, FileMode.Open);
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave" + index + ".sav", FileMode.Open);
        //反序列化
        //BinaryFormatter bf = new BinaryFormatter();
        //SaveInfo saveInfo = (SaveInfo)bf.Deserialize(file);
        BinaryReader reader = new BinaryReader(file);
        //string note = new string(reader.ReadChars(20));
        //Debug.Log(note);
        string note = reader.ReadString();
        string saveJSON = reader.ReadString();
        SaveInfo saveInfo = JsonUtility.FromJson<SaveInfo>(saveJSON);
        reader.Close();
        file.Close();
        OnLoad?.Invoke(saveInfo);
        Debug.Log("加载游戏");
    }
    public static List<string> LoadMessage()
    {
        List<string> res = new List<string>();
        string[] filepaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (var path in filepaths)
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryReader reader = new BinaryReader(file);
            string note = reader.ReadString();
            reader.Close();
            file.Close();
            res.Add(note);
        }
        return res;
    }
}