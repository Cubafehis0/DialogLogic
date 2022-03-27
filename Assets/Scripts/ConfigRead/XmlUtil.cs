using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XmlUtil
{
    public static int GetInt(XmlElement e)
    {
        if (e == null) return 0;
        int.TryParse(e.InnerText, out int res);
        return res;
    }
    public static Personality GetPersonality(XmlElement list)
    {
        Personality personality = new Personality();
        personality.Logic = GetInt(list["logic"]);
        personality.Moral = GetInt(list["moral"]);
        personality.Roundabout = GetInt(list["roundabout"]);
        personality.Inner = GetInt(list["inner"]);
        personality.Outside = GetInt(list["outside"]);
        return personality;
    }
    public static string GetString(XmlElement e)
    {
        return e.InnerText;
    }
    public static void GetCardInfo(XmlElement e, out string name, out int num)
    {
        //默认值设置为0
        name = "";
        num = 1;
        XmlNodeList list = e.ChildNodes;
        foreach (XmlElement item in list)
        {
            switch(item.Name)
            {
                case "name":
                    name = GetString(item);
                    break;
                case "num":
                    num = GetInt(item);
                    break;
            }
        }
    }

    public static void ParseCardInfo(XmlElement e, out string name, out int num)
    {
        name = e["name"].InnerText;
        num = GetInt(e["num"]);
    }
}
