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
    [MenuItem("表格/生成卡牌预制体")]
    static void CreateCardPrefab()
    {
        Debug.Log("Create Card Prefab");
        if(!File.Exists(cardTablePath) && !File.Exists(effectTablePath))
        {
            Debug.LogWarning("缺少相应的表格信息");
            return;
        }
        CardTable cardTable = ScriptableAssetManage.GetScriptableObject("CardTable") as CardTable;
        EffectTable effectTable= ScriptableAssetManage.GetScriptableObject("EffectTable") as EffectTable;
        EffectDesc.InitalDic(effectTable);
        GameObject cardBase= AssetDatabase.LoadAssetAtPath(cardBasePath, typeof(GameObject)) as GameObject;
        CreateCardPool(cardTable.cardpool_ags,nameof(cardTable.cardpool_ags), cardBase);
        CreateCardPool(cardTable.cardpool_imm, nameof(cardTable.cardpool_imm), cardBase);
        CreateCardPool(cardTable.cardpool_lgc, nameof(cardTable.cardpool_lgc), cardBase);
        CreateCardPool(cardTable.cardpool_mrl, nameof(cardTable.cardpool_mrl), cardBase);
        CreateCardPool(cardTable.cardpool_rdb, nameof(cardTable.cardpool_rdb), cardBase);
        CreateCardPool(cardTable.cardpool_spt, nameof(cardTable.cardpool_spt), cardBase);

    }

    private static void CreateCardPool(List<CardEntity> cardpool, string poolName, GameObject cardBase)
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
            
            //cardObject.GetComponent<Card>().info=new CardInfo(entity);
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

    private static int GetCardTypeByCardPoolName(string pool)
    {
        switch (pool)
        {
            case "cardpool_ags":
                return CardCategory.Ags;
            case "cardpool_imm":
                return CardCategory.Imm;
            case "cardpool_lgc":
                return CardCategory.Lgc;
            case "cardpool_mrl":
                return CardCategory.Mrl;
            case "cardpool_rdb":
                return CardCategory.Rdb;
            case "cardpool_spt":
                return CardCategory.Spt;
        }
        return CardCategory.Ags;
    }
}
