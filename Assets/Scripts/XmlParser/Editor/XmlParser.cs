using UnityEditor;

public class XmlParser
{
    //readonly static string XMLSCHEMAPATH = @"C:\Users\Admin\Desktop\Xml\test\cardTest.xsd";
    //readonly static string CARDXMLPATH = @"C:\Users\Admin\Desktop\Xml\test\cardTest.xml";

    //readonly static string cardBasePath = "Assets//Prefabs//Cards//Base.prefab";
    //readonly static string cardDirPath = "Assets//Prefabs//Cards";


    [MenuItem("Xml����/���ɿ���Ԥ����")]
    // Start is called before the first frame update
    static void CreateCardsPrefab()
    {
        //XmlDocument doc=XmlDocumentHelper.LoadDocumentWithSchemaValidation(CARDXMLPATH, XMLSCHEMAPATH);
        //if (doc == null)
        //{
        //    Debug.Log(string.Format("�޷���λ��{0}��xml�ļ�", CARDXMLPATH));
        //    return;
        //}
        //List<Card> cards = XmlDocumentHelper.ParseCardXml(doc);
        //GameObject cardBase = AssetDatabase.LoadAssetAtPath(cardBasePath, typeof(GameObject)) as GameObject;
        //GameObject cardObject = GameObject.Instantiate(cardBase);
        //foreach (var card in cards)
        //{
        //    //cardObject.GetComponent<Card>().Construct(card);
        //    //cardObject.GetComponent<CardObject>().GetCardComponent();
        //    cardObject.GetComponent<CardObject>().UpdateVisuals();
        //    string fileName = card.info.Title + ".prefab";
        //    string filePath = Path.Combine(cardDirPath, fileName);
        //    PrefabUtility.SaveAsPrefabAsset(cardObject, filePath, out bool success);
        //}
        //GameObject.DestroyImmediate(cardObject);
    }

}
