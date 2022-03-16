using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class ExcelImporter : AssetPostprocessor
{
	class ExcelAssetInfo
	{
		public Type AssetType { get; set; }
		public ExcelAssetAttribute Attribute { get; set; } 
		public string ExcelName
		{
			get
			{
				return string.IsNullOrEmpty(Attribute.ExcelName) ? AssetType.Name : Attribute.ExcelName;
			}
		}
	}

	static List<ExcelAssetInfo> cachedInfos = null; // Clear on compile.


	[MenuItem("表格/导入条件表", priority = 1)]
	static void ImportEffectTable()
    {
		Debug.Log("Import Effect Table");
		string path = BrowserHelper.OpenProject("导入条件表",BrowserHelper.EXCELFILTER);
		if (path == null) return;
		ImportTable("EffectTable", path);
		EffectTable effectTable = LoadOrCreateAsset("Assets//ExcelAssets//EffectTable.asset", typeof(EffectTable)) as EffectTable;
		EffectDesc.InitalDic(effectTable);
	}
	[MenuItem("表格/导入卡牌表", priority = 2)]
	static void ImportCardTable()
    {
		Debug.Log("Import Card Table"); string path = BrowserHelper.OpenProject("导入卡牌表", BrowserHelper.EXCELFILTER);
		if (path == null) return;
		ImportTable("CardTable", path);
	}
    
	
	//[MenuItem("表格/导入任务表", priority = 3)]
 //   static void ImportTaskTable()
 //   {
 //       Debug.Log("Import Task Table"); string path = BrowserHelper.OpenProject("导入任务表", BrowserHelper.EXCELFILTER);
 //       if (path == null) return;
 //       ImportTable("TaskEntityTable", path);
 //   }

    static void ImportTable(string excelName,string path)
	{
		if (cachedInfos == null) cachedInfos = FindExcelAssetInfos();
		ExcelAssetInfo info = cachedInfos.Find(i => i.ExcelName == excelName);
		if (info == null) return;
		ImportExcel(path, info);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	//static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	//{
	//	Debug.Log("import");
	//	bool imported = false;
	//	foreach(string path in importedAssets)
	//	{
	//		if(Path.GetExtension(path) == ".xls" || Path.GetExtension(path) == ".xlsx") 
	//		{
	//			var excelName = Path.GetFileNameWithoutExtension(path);
	//			if(excelName.StartsWith("~$")) continue;

	//			ExcelAssetInfo info = cachedInfos.Find(i => i.ExcelName == excelName);

	//			if(info == null) continue;

	//			ImportExcel(path, info);
	//			imported = true;
	//		}
	//	}

	//	if(imported) 
	//	{
	//		AssetDatabase.SaveAssets();
	//		AssetDatabase.Refresh();
	//	}
	//}

	static List<ExcelAssetInfo> FindExcelAssetInfos()
	{
		var list = new List<ExcelAssetInfo>();
		foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			foreach(var type in assembly.GetTypes())
			{
				var attributes = type.GetCustomAttributes(typeof(ExcelAssetAttribute), false);
				if(attributes.Length == 0) continue;
				var attribute = (ExcelAssetAttribute)attributes[0];
				var info = new ExcelAssetInfo()
				{
					AssetType = type,
					Attribute = attribute
				};
				list.Add(info);
			}
		}
		return list;
	}

	public static UnityEngine.Object LoadOrCreateAsset(string assetPath, Type assetType)
	{
		Directory.CreateDirectory(Path.GetDirectoryName(assetPath));

		var asset = AssetDatabase.LoadAssetAtPath(assetPath, assetType);

		if (asset == null)
		{
			asset = ScriptableObject.CreateInstance(assetType.Name);
			AssetDatabase.CreateAsset((ScriptableObject)asset, assetPath);
			asset.hideFlags = HideFlags.NotEditable;
		}

		return asset;
	}

	public static IWorkbook LoadBook(string excelPath)
	{
		using(FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			if (Path.GetExtension(excelPath) == ".xls") return new HSSFWorkbook(stream);
			else return new XSSFWorkbook(stream);
		}
	}
	public static List<string> GetSheetNames(IWorkbook book)
	{
		var sheetNames = new List<string>();
		for (int i = 0; i < book.NumberOfSheets; i++)
		{
			var sheet = book.GetSheetAt(i);
			sheetNames.Add(sheet.SheetName);
		}
		return sheetNames;
	}
	public static List<string> GetFieldNamesFromSheetHeader(ISheet sheet)
	{
		IRow headerRow = sheet.GetRow(0);

		var fieldNames = new List<string>();
		for (int i = 0; i < headerRow.LastCellNum; i++)
		{

			var cell = headerRow.GetCell(i);
			if (cell == null || cell.CellType == CellType.Blank)
            {
				fieldNames.Add("");
				continue;
			}
			fieldNames.Add(cell.StringCellValue);
		}
		return fieldNames;
	}

	static object CellToFieldObject(ICell cell, FieldInfo fieldInfo, bool isFormulaEvalute = false)
	{
		var type = isFormulaEvalute ? cell.CachedFormulaResultType : cell.CellType;

		switch(type)
		{
			case CellType.String:
				if (fieldInfo.FieldType.IsEnum) return Enum.Parse(fieldInfo.FieldType, cell.StringCellValue);
				else return cell.StringCellValue;
			case CellType.Boolean:
				return cell.BooleanCellValue;
			case CellType.Numeric:
				return Convert.ChangeType(cell.NumericCellValue, fieldInfo.FieldType);
			case CellType.Formula:
				if(isFormulaEvalute) return null;
				return CellToFieldObject(cell, fieldInfo, true); 
			default:
				if(fieldInfo.FieldType.IsValueType)
				{
					return Activator.CreateInstance(fieldInfo.FieldType);
				}
				return null;
		}
	}

	static object CreateEntityFromRow(IRow row, List<string> columnNames, Type entityType, string sheetName)
	{
		var entity = Activator.CreateInstance(entityType);
		int cnt = 0;
        for (int i = 0; i < row.LastCellNum; i++)
        {
			if (columnNames[i] == "") continue;
			var cell = row.GetCell(i);
			
			if (cell == null || cell.CellType == CellType.Blank) continue;
			FieldInfo entityField = entityType.GetField(
				columnNames[i],
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
			);
			if (entityField == null) continue;
			if (!entityField.IsPublic && entityField.GetCustomAttributes(typeof(SerializeField), false).Length == 0) continue;
			try
			{
				object fieldValue = CellToFieldObject(cell, entityField);
				entityField.SetValue(entity, fieldValue);
			}
			catch
			{
				throw new Exception(string.Format("Invalid excel cell type at row {0}, column {1}, {2} sheet.", row.RowNum, cell.ColumnIndex, sheetName));
			}
		}
		return entity;
	}
	static object GetEntityListFromSheet(ISheet sheet, Type entityType)
	{
		List<string> excelColumnNames = GetFieldNamesFromSheetHeader(sheet);

		Type listType = typeof(List<>).MakeGenericType(entityType);
		MethodInfo listAddMethod = listType.GetMethod("Add", new Type[]{entityType});
		object list = Activator.CreateInstance(listType);

		// row of index 0 is header
		for (int i = 1; i <= sheet.LastRowNum; i++)
		{
			IRow row = sheet.GetRow(i);
			if(row == null) continue;

			ICell entryCell = row.GetCell(0); 
			if(entryCell == null || entryCell.CellType == CellType.Blank) continue;

			// skip comment row
			if(entryCell.CellType == CellType.String && entryCell.StringCellValue.StartsWith("#")) continue;

			var entity = CreateEntityFromRow(row, excelColumnNames, entityType, sheet.SheetName);
			listAddMethod.Invoke(list, new object[] { entity });
		}
		return list;
	}

	static void ImportExcel(string excelPath, ExcelAssetInfo info)
	{
		string assetName = info.AssetType.Name + ".asset";
		string assetPath = Path.Combine("Assets//ExcelAssets",assetName);
		UnityEngine.Object asset = LoadOrCreateAsset(assetPath, info.AssetType);
		IWorkbook book = LoadBook(excelPath);

		var assetFields = info.AssetType.GetFields();
		int sheetCount = 0;
		foreach (var assetField in assetFields)
		{
			ISheet sheet =  book.GetSheet(assetField.Name);
			if(sheet == null) continue;

			Type fieldType = assetField.FieldType;
			if(! fieldType.IsGenericType || (fieldType.GetGenericTypeDefinition() != typeof(List<>))) continue;

			Type[] types = fieldType.GetGenericArguments();
			Type entityType = types[0];

			object entities = GetEntityListFromSheet(sheet, entityType);
			assetField.SetValue(asset, entities);
			sheetCount++;
		}

		if(info.Attribute.LogOnImport)
		{
			Debug.Log(string.Format("Imported {0} sheets form {1}.", sheetCount, excelPath));
		}

		EditorUtility.SetDirty(asset);
	}
}
