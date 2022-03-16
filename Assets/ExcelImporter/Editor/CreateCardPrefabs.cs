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
    [MenuItem("������/���ɿ���Ԥ����", priority = 2)]
    static void CreateCardPrefab()
    {
        Debug.Log("Create Card Prefab");
        if(!File.Exists(cardTablePath) && !File.Exists(effectTablePath))
        {
            Debug.LogWarning("ȱ����Ӧ�ı����Ϣ");
            return;
        }
        CardTable cardTable = ExcelImporter.LoadOrCreateAsset(cardTablePath, typeof(CardTable)) as CardTable;
        EffectTable effectTable= ExcelImporter.LoadOrCreateAsset(effectTablePath, typeof(EffectTable)) as EffectTable;
        EffectDesc.InitalDic(effectTable);
        GameObject cardBase= AssetDatabase.LoadAssetAtPath(cardBasePath, typeof(GameObject)) as GameObject;
        CreateCardPool(cardTable.cardpool_ags,nameof(cardTable.cardpool_ags), effectTable, cardBase);
        CreateCardPool(cardTable.cardpool_imm, nameof(cardTable.cardpool_imm), effectTable, cardBase);
        CreateCardPool(cardTable.cardpool_lgc, nameof(cardTable.cardpool_lgc), effectTable, cardBase);
        CreateCardPool(cardTable.cardpool_mrl, nameof(cardTable.cardpool_mrl), effectTable, cardBase);
        CreateCardPool(cardTable.cardpool_rdb, nameof(cardTable.cardpool_rdb), effectTable, cardBase);
        CreateCardPool(cardTable.cardpool_spt, nameof(cardTable.cardpool_spt), effectTable, cardBase);
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
                Debug.LogError(string.Format("����{0}�е�{1}���������ֲ����ڣ��Ѿ�ֹͣ������Ԥ����",poolName,i+1));
                continue;
            }
            string fileName = entity.name + ".prefab";
            string filePath = Path.Combine(dirPath, fileName);
            if (File.Exists(filePath))
            {
                Debug.Log(string.Format("����{0}�е�{1}������Ϊ{2}�����Ѿ����ڣ��Ѿ������滻���µ�Ԥ����", poolName, i+1,entity.name));
                File.Delete(filePath);
            }
            cardObject.GetComponent<Card>().info=new CardInfo(entity);
            CardObject card = cardObject.GetComponent<CardObject>();
            card.GetCardComponent();
            card.UpdateVisuals();
            PrefabUtility.SaveAsPrefabAsset(cardObject, filePath,out bool success);
            if(!success)
            {
                Debug.LogError(string.Format("Ϊ����Ϊ{0}�Ŀ��ƴ���Ԥ����ʧ��",entity.name));
            }
        }
        GameObject.DestroyImmediate(cardObject);
    }
}
