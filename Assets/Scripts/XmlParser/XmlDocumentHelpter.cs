using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Xml;
using static System.Console;
using UnityEngine;
public class XmlDocumentHelpter
{
    const string GRAMMARNS = "JaspersNodeGrammar";
    public static XmlDocument LoadDocumentWithSchemaValidation(string xmlPath, string schamaPath)
    {
        XmlReader reader;
        XmlReaderSettings settings = new XmlReaderSettings();
        XmlSchema schema = GetSchema(schamaPath);
        if (schema == null)
        {
            return null;
        }
        settings.Schemas.Add(schema);
        settings.ValidationEventHandler += SettingsValidationEventHandler;
        settings.ValidationFlags =
            settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
        settings.ValidationType = ValidationType.Schema;
        try
        {
            reader = XmlReader.Create(xmlPath, settings);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
        XmlDocument doc = new XmlDocument();
        doc.PreserveWhitespace = false;
        doc.Load(reader);
        reader.Close();
        return doc;
    }

    public static List<Card> ParseCardXml(XmlDocument doc)
    {
        XmlElement root = doc.DocumentElement;
        XmlNode n = root.FirstChild;
        XmlNodeList cardsDefine = root.GetElementsByTagName("define_card");
        List<Card> res = ParseCardDefineNode(cardsDefine);
        //XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        //nsmgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema-instance");

        //XmlNodeList cardsDefine = root.SelectNodes("/root/define_card",nsmgr);
        return res;
    }
    static List<Card> ParseCardDefineNode(XmlNodeList cardsDefine)
    {
        List<Card> res = new List<Card>();
        foreach(XmlElement e in cardsDefine)
        {
            string title= e["title"].InnerText;
            string desc = e["desc"].InnerText;
            string meme = e["meme"].InnerText;
            Card card = new Card() { title=title,meme=meme};
            res.Add(card);
        }
        return res;
    }
    public static XmlNode FindXmlNodeInList(XmlNodeList nodeList, Predicate<XmlNode> predicate)
    {
        foreach(XmlNode node in nodeList)
        {
            if (predicate(node)) return node;
        }
        return null;
    }
    private static XmlSchema GetSchema(string schemaPath)
    {
        XmlSchemaSet xs = new XmlSchemaSet();
        XmlSchema schema;
        try
        {
            schema = xs.Add(GRAMMARNS, schemaPath);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
        return schema;
    }
    private static void ValidateXML(XmlDocument doc)
    {
        if (doc.Schemas.Count == 0)
        {
            Debug.Log("该文档没有对应的xsd验证");
            return;
        }
        doc.Validate(SettingsValidationEventHandler);
        Debug.Log("完成对xml文件的验证");
    }
    private static void SettingsValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Warning)
            Debug.Log("The following validation warning occurred: " + e.Message);
        else if (e.Severity == XmlSeverityType.Error)
            Debug.Log("The following critical validation errors occurred: " + e.Message);
    }
}