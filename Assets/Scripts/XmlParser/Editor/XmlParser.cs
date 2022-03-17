using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEditor;
using System.IO;

public class XmlParser
{
    readonly static string XMLSCHEMAPATH = @"C:\Users\Admin\Desktop\Xml\test\cardTest.xsd";
    readonly static string CARDXMLPATH = @"C:\Users\Admin\Desktop\Xml\test\cardTest.xml";

    readonly static string cardBasePath = "Assets//Prefabs//Cards//Base.prefab";
    readonly static string cardDirPath = "Assets//Prefabs//Cards";


    [MenuItem("Xml解析/生成卡牌预制体")]
    // Start is called before the first frame update
    static void CreateCardsPrefab()
    {
        XmlDocument doc=XmlDocumentHelper.LoadDocumentWithSchemaValidation(CARDXMLPATH, XMLSCHEMAPATH);
        if (doc == null)
        {
            Debug.Log(string.Format("无法打开位于{0}的xml文件", CARDXMLPATH));
            return;
        }
        List<Card> cards = XmlDocumentHelper.ParseCardXml(doc);
        GameObject cardBase = AssetDatabase.LoadAssetAtPath(cardBasePath, typeof(GameObject)) as GameObject;
        GameObject cardObject = GameObject.Instantiate(cardBase);
        foreach (var card in cards)
        {
            cardObject.GetComponent<Card>().Construct(card);
            cardObject.GetComponent<CardObject>().GetCardComponent();
            cardObject.GetComponent<CardObject>().UpdateVisuals();
            string fileName = card.info.title + ".prefab";
            string filePath = Path.Combine(cardDirPath, fileName);
            PrefabUtility.SaveAsPrefabAsset(cardObject, filePath, out bool success);
        }
        GameObject.DestroyImmediate(cardObject);
    }

}
