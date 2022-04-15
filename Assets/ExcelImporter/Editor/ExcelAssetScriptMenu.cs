using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ExcelAssetScriptMenu
{
	const string TableScriptTemplateName = "ExcelTableScriptTemplete.cs.txt";
	const string TableFieldTemplete = "\tpublic List<#EntityType#> #FIELDNAME#;";
	const string EntityScriptTemplateName = "ExcelEntityScriptTemplate.cs.txt";
	const string EntityFieldTemplete = "\tpublic string #FIELDNAME#;";
	[MenuItem("表格/创建配置脚本信息", priority = 0)]
	static void CreateScript()
	{
		string savePath = EditorUtility.SaveFolderPanel("选择生成的excel配置脚本保存位置", Application.dataPath, "");
		if (savePath == "") return;

		var selectedAssets = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);

		string excelPath = AssetDatabase.GetAssetPath(selectedAssets[0]);
		string excelName = Path.GetFileNameWithoutExtension(excelPath);

		IWorkbook book = ExcelImporter.LoadBook(excelPath);
		List<string> sheetNames = ExcelImporter.GetSheetNames(book);
		List<string> fieldNames = ExcelImporter.GetFieldNamesFromSheetHeader(book.GetSheetAt(0)).FindAll(s => !string.IsNullOrEmpty(s));

		string className = excelName + "Entity";
		string scriptString = BulidAssetScriptString(className, fieldNames, EntityScriptTemplateName, EntityFieldTemplete);
		string path = Path.ChangeExtension(Path.Combine(savePath, className), "cs");
		WriteText2File(path, scriptString);

		string sheetField = String.Copy(TableFieldTemplete);
		sheetField = sheetField.Replace("#EntityType#", className);
		className = excelName + "EntityTable";
		scriptString = BulidAssetScriptString(className, sheetNames, TableScriptTemplateName, sheetField);
		path = Path.ChangeExtension(Path.Combine(savePath, className), "cs");
		WriteText2File(path, scriptString);
		AssetDatabase.Refresh();
		Debug.Log(string.Format("成功在{0}位置为{1}表格创建了配置脚本", savePath, excelName));
	}

	[MenuItem("表格/创建配置脚本信息", true)]
	static bool CreateScriptValidation()
	{
		var selectedAssets = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
		if (selectedAssets.Length != 1) return false;
		var path = AssetDatabase.GetAssetPath(selectedAssets[0]);
		return Path.GetExtension(path) == ".xls" || Path.GetExtension(path) == ".xlsx";
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
	static string BulidAssetScriptString(string excelName, List<string> fieldNames, string templateName, string filedTemplate)
	{
		string scriptString = GetScriptTempleteString(templateName);
		scriptString = scriptString.Replace("#CLASSNAME#", excelName);

		foreach (string sheetName in fieldNames)
		{
			string fieldString = String.Copy(filedTemplate);
			fieldString = fieldString.Replace("#FIELDNAME#", sheetName);
			fieldString += "\n#FIELDS#";
			scriptString = scriptString.Replace("#FIELDS#", fieldString);
		}
		scriptString = scriptString.Replace("\n#FIELDS#", "");
		return scriptString;
	}

}