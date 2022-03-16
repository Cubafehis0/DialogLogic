using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class XmlParser : MonoBehaviour
{
    const string XMLSCHEMAPATH = @"C:\Users\Admin\Desktop\Xml\test.xsd";
    const string CARDXMLPATH = @"C:\Users\Admin\Desktop\Xml\test.xml";
    // Start is called before the first frame update
    void Start()
    {
        XmlDocument doc=XmlDocumentHelpter.LoadDocumentWithSchemaValidation(CARDXMLPATH, XMLSCHEMAPATH);
        if (doc == null) Debug.Log(string.Format("无法打开位于{0}的xml文件", CARDXMLPATH));
        else
        {
            List<Card> cards = XmlDocumentHelpter.ParseCardXml(doc);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
