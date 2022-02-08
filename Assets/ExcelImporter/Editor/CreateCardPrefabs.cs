using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CreateCardPrefabs
{
    readonly static string cardTablePath = "Assets//ExcelAssets//CardTable.asset";
    readonly static string effectTablePath = "Assets//ExcelAssets//EffectTable.asset";
    readonly static string cardBasePath= "Assets//Prefabs//Cards//Base.prefab";
    readonly static string cardDirPath = "Assets//Prefabs//Cards";
    [MenuItem("导入表格/生成卡牌预制体", priority = 2)]
    static void CreateCardPrefab()
    {
        Debug.Log("Create Card Prefab");
        if(!File.Exists(cardTablePath) && !File.Exists(effectTablePath))
        {
            Debug.LogWarning("缺少相应的表格信息");
            return;
        }
        CardTable cardTable = ExcelImporter.LoadOrCreateAsset(cardTablePath, typeof(CardTable)) as CardTable;
        EffectTable effectTable= ExcelImporter.LoadOrCreateAsset(effectTablePath, typeof(EffectTable)) as EffectTable;
        EffectDesc.InitalDic(effectTable);
        GameObject cardBase= AssetDatabase.LoadAssetAtPath(cardBasePath, typeof(GameObject)) as GameObject;
        CreateCardPool(cardTable.cardpool_ags,nameof(cardTable.cardpool_ags), effectTable, cardBase);
        //CreateCardPool(cardTable.cardpool_imm, effectTable, cardBase);
        //CreateCardPool(cardTable.cardpool_lgc, effectTable, cardBase);
        //CreateCardPool(cardTable.cardpool_mrl, effectTable, cardBase);
        //CreateCardPool(cardTable.cardpool_rdb, effectTable, cardBase);
        //CreateCardPool(cardTable.cardpool_spt, effectTable, cardBase);
    }
    
    private static void CreateCardPool(List<CardEntity> cardpool, string poolName, EffectTable effectTable, GameObject cardBase)
    {
        string dirPath = Path.Combine(cardDirPath, poolName);
        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
        GameObject cardObject = GameObject.Instantiate(cardBase);
        for (int i=0;i<cardpool.Count;i++)
        {
            var entity = cardpool[i];
            if(string.IsNullOrEmpty(entity.name))
            {
                Debug.LogError(string.Format("卡池{0}中第{1}个卡牌名字不存在，已经停止创建其预制体",poolName,i+1));
                continue;
            }
            string fileName = entity.name + ".prefab";
            string filePath = Path.Combine(dirPath, fileName);
            if (File.Exists(filePath))
            {
                Debug.Log(string.Format("卡池{0}中第{1}个名称为{2}卡牌已经存在，已经将其替换成新的预制体", poolName, i+1,entity.name));
                File.Delete(filePath);
            }
            cardObject.GetComponent<Card>().Refresh(entity);
            CardObject card = cardObject.GetComponent<CardObject>();
            card.GetCardComponent();
            card.UpdateVisuals();
            PrefabUtility.SaveAsPrefabAsset(cardObject, filePath,out bool success);
            if(!success)
            {
                Debug.LogError(string.Format("为名称为{0}的卡牌创建预制体失败",entity.name));
            }
        }
        GameObject.DestroyImmediate(cardObject);
    }
}
