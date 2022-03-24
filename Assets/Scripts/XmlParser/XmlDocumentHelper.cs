using SemanticTree;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using UnityEngine;
using SemanticTree.PlayerEffect;
using SemanticTree.Expression;

public class XmlDocumentHelper
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
        return res;
    }
    static List<Card> ParseCardDefineNode(XmlNodeList cardsDefine)
    {
        List<Card> cards = new List<Card>();
        //CardInfoTable cardInfoTable = ScriptableAssetManage.GetScriptableObject(typeof(CardInfoTable)) as CardInfoTable;
        foreach(XmlElement e in cardsDefine)
        {
            string title= e["title"].InnerText;
            string conditionDesc = e["condition_desc"]?.InnerText;
            string effectDesc = e["effect_desc"].InnerText;
            string meme = e["meme"].InnerText;
            XmlNode node = e.GetElementsByTagName("effect")[0];
            Card card = new Card();
            //card.info = new CardInfo() { title = title, condtionDesc = conditionDesc, effectDesc = effectDesc, meme = meme };
            //if(node["condition"]!=null) card.conditionNode = ParseCardConditions(node["condition"]);
            //if (node["pull_effect"] != null) card.pullEffectNode = ParseCardEffects(node["pull_effect"]);
            //if (node["hold_effect"] != null) card.pullEffectNode = ParseCardEffects(node["hold_effect"]);
            //if (node["play_effect"] != null) card.pullEffectNode = ParseCardEffects(node["play_effect"]);
            XmlNodeList nodeList = e.GetElementsByTagName("define_card_var");
            cards.Add(card);
        }
        return cards;
    }

    public static Dictionary<string,IExpression> ParseCardVar(XmlNodeList nodeList)
    {
        Dictionary<string, IExpression> res = new Dictionary<string, IExpression>();

        return res;
    }

    public static List<ICondition> ParseCardConditions(XmlNode xmlNode)
    {
        return null;
    }


    public static IEffectNode ParseEffect(XmlNode xmlNode)
    {
        IEffectNode ret = xmlNode.Name switch
        {
            "modify_personality" => new ModifyPersonalityNode(xmlNode),
            _ => null,
        };
        return ret;
        
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