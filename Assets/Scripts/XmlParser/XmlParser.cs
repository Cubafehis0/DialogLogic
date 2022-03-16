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
        if (doc == null) Debug.Log(string.Format("�޷���λ��{0}��xml�ļ�", CARDXMLPATH));
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
