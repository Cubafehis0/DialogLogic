using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]
public class SaveInfo
{
    public string inkState;
    //���״̬
    //public Player player;
    
    //������Ϣ
    //public List<T> cardInHand;
    //public List<T> cardInDeck;
    //public List<T> cardInGrave;
}
public abstract class Save
{
    public static void SaveGame()
    {
        SaveInfo saveInfo = CreateSaveInfoObject();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.sav");
        //���л�����
        //BinaryFormatter bf = new BinaryFormatter();
        //bf.Serialize(file, saveInfo);

        //����ΪJSON
        string save2Json = JsonUtility.ToJson(saveInfo);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(save2Json);
        writer.Close();
        file.Close(); 
        Debug.Log("Game Saved");

    }
    private static SaveInfo CreateSaveInfoObject()
    {
        SaveInfo saveInfo = new SaveInfo();
        saveInfo.inkState = Ink2Unity.InkStory.NowStory.NowState2Json();
        //saveInfo.player=
        //���濨����Ϣ
        return saveInfo;
    }
}
public abstract class Load
{
    public static void LoadGame(string fileName=null)
    {
        
        //FileStream file = File.Open(Application.persistentDataPath + "/"+fileName, FileMode.Open);
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.sav", FileMode.Open);
        //�����л�
        //BinaryFormatter bf = new BinaryFormatter();
        //SaveInfo saveInfo = (SaveInfo)bf.Deserialize(file);
        BinaryReader reader = new BinaryReader(file);
        string saveJSON = reader.ReadString();
        SaveInfo saveInfo = JsonUtility.FromJson<SaveInfo>(saveJSON);
        file.Close();
        string state = saveInfo.inkState;
        Ink2Unity.InkStory.NowStory.LoadStory(state);
        //
        //������Ϣ����
        //
        Debug.Log("������Ϸ");
    }
}

