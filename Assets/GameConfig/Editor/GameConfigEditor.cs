using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameConfigEditor
{
    const string GameConfigTestTemplate = "TestConfig.xml.txt";
    [MenuItem("Xml����/�����ؿ�������Ϣ", priority = 0)]
    private static void CreateConfigXml()
    {
        string savePath = EditorUtility.SaveFilePanel("ѡ��Xml�ļ��ı���Ŀ¼", Application.dataPath, "", "xml");
        if (savePath == "") return;
        string s = GetScriptTempleteString(GameConfigTestTemplate);
        Path.ChangeExtension(savePath, "xml");
        WriteText2File(savePath, s);
        AssetDatabase.Refresh();
        Debug.Log(string.Format("�ɹ���{0}λ��Ϊ��񴴽��˹ؿ����������ļ�", savePath));
    }
    static void WriteText2File(string path, string text)
    {
        if (File.Exists(path))
            File.Delete(path);
        File.WriteAllText(path, text);
    }
    static string GetScriptTempleteString(string templateName)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string[] filePath = Directory.GetFiles(currentDirectory, templateName, SearchOption.AllDirectories);
        if (filePath.Length <= 0) throw new Exception("Script template not found.");

        string templateString = File.ReadAllText(filePath[0]);
        return templateString;
    }

}
