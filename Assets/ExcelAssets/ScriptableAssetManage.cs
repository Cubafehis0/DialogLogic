using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptableAssetManage : MonoBehaviour
{
    readonly static string CardTablePath = "Assets//ExcelAssets//CardTable.asset";
    readonly static string EffectTablePath = "Assets//ExcelAssets//EffectTable.asset";
    //readonly static string CardInfoTablePath = "Assets//ExcelAssets//CardInfoTable.asset";
    public static UnityEngine.Object GetScriptableObject(Type type)
    {
        return GetScriptableObject(type.Name);

    }
    public static UnityEngine.Object GetScriptableObject(string typeName)
    {
        return typeName switch
        {
            "CardTable" => LoadOrCreateAsset(CardTablePath, typeof(CardTable)),
            "EffectTable" => LoadOrCreateAsset(EffectTablePath, typeof(EffectTable)),
            //"CardInfoTable" => LoadOrCreateAsset(EffectTablePath, typeof(CardInfoTable)),
            _ => null,
        };

    }
    private static UnityEngine.Object LoadOrCreateAsset(string assetPath, Type assetType)
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
}
