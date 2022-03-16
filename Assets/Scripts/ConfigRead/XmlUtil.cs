using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class XmlUtil
{
    public static int GetInt(XmlElement e)
    {
        int.TryParse(e.InnerText, out int res);
        return res;
    }
    public static int[] GetCharacter(XmlElement e)
    {
        int[] res = new int[4];
        XmlNodeList list = e.ChildNodes;
        foreach (XmlElement n in list)
        {
            int i = 0;
            switch (n.Name)
            {
                case "lgc":
                    i = 0;
                    break;
                case "mrl":
                    i = 1;
                    break;
                case "rdb":
                    i = 2;
                    break;
                case "inn":
                    i = 3;
                    break;
            }
            int.TryParse(n.InnerText, out res[i]);
        }
        return res;
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
}
