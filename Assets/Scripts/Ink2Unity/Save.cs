using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]
public class SaveInfo
{
    public string inkState;
    //玩家状态
    //public Player player;
    
    //卡牌信息
    //public List<T> cardInHand;
    //public List<T> cardInDeck;
    //public List<T> cardInGrave;
}
public abstract class Save
{
    public static void SaveGame(int index,string note= "")
    {
        SaveInfo saveInfo = CreateSaveInfoObject();
        string path = Application.persistentDataPath + "/gamesave" + index + ".sav";
        if (File.Exists(path))
            File.Delete(path);  
        FileStream file = File.Create(path);
        //序列化保存
        //BinaryFormatter bf = new BinaryFormatter();
        //bf.Serialize(file, saveInfo);

        //保存为JSON
        string save2Json = JsonUtility.ToJson(saveInfo);
        BinaryWriter writer = new BinaryWriter(file);
        if (note.Length > 20)
        {
            Debug.LogError("输入存档备份信息过长,存档失败");
            writer.Close();
            file.Close();
            return;
        }
        for(int i=note.Length;i<20;i++)
        {
            note = note + ' ';
        }
        writer.Write(note);
        writer.Write(save2Json);
        Debug.Log(note);
        Debug.Log(save2Json);
        writer.Close();
        file.Close();
        Debug.Log("Game Saved In"+ Application.persistentDataPath + "/gamesave" + index + ".sav");
    }
    private static SaveInfo CreateSaveInfoObject()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.inkState = Ink2Unity.InkStory.NowStory.NowState2Json();
        //saveInfo.player=
        //保存卡牌信息
        return saveInfo;
    }
}

public interface ILoad
{
    void LoadGame(int index);
    List<string> LoadMessage();
}
public class Load:ILoad
{

    public List<string> LoadMessage()
    {
        List<string> res = new List<string>();
        string[] filepaths= Directory.GetFiles(Application.persistentDataPath);
        foreach(var path in filepaths)
        {

            FileStream file = File.Open(path,FileMode.Open);
            BinaryReader reader = new BinaryReader(file);
            string note = reader.ReadString();
            reader.Close();
            file.Close();
            res.Add(note);
        }
        return res;
    }
    public void LoadGame(int index)
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
        string state = saveInfo.inkState;
        Ink2Unity.InkStory.NowStory.LoadStory(state);
        //
        //其他信息加载
        //
        Debug.Log("加载游戏");
    }
}

