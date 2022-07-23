using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class GameConfigEditor
{
	const string GameConfigTestTemplate = "TestConfig.xml.txt";
	[MenuItem("Xml解析/创建关卡配置信息", priority = 0)]
    private static void CreateConfigXml()
    {
		string savePath=EditorUtility.SaveFilePanel("选择Xml文件的保存目录", Application.dataPath, "", "xml");
		if (savePath == "") return;
		string s = GetScriptTempleteString(GameConfigTestTemplate);
		Path.ChangeExtension(savePath, "xml");
		WriteText2File(savePath, s);
		AssetDatabase.Refresh();
		Debug.Log(string.Format("成功在{0}位置为表格创建了关卡测试配置文件", savePath));
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
