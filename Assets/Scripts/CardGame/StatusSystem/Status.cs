using System;
using System.Collections.Generic;
using System.Xml;
using SemanticTree;

public class GameScript
{
    protected string name;
    public EffectList OnAfterPlayCard = null;
    public EffectList OnTurnStart = null;
}

public class Status : GameScript
{
    //可能替换为StatusInfo结构体
    private int decreaseOnTurnStart = 0; //回合开始自动减少
    private int decreaseOnTurnEnd = 0;  //回合结束自动减少
    private bool stackable = true;   //
    private bool allowNegative = false;

    public EffectList OnAdd = null;
    public EffectList OnRemove = null;

    public string Name { get => name; }
    public int DecreaseOnTurnStart { get => decreaseOnTurnStart; }
    public int DecreaseOnTurnEnd { get => decreaseOnTurnEnd; }
    public bool Stackable { get => stackable; }
    public bool AllowNegative { get => allowNegative; }

    public void Construct(XmlNode xmlNode)
    {
        if (!xmlNode.Name.Equals("define_status")) throw new SemanticException();
        name = xmlNode.Attributes["name"].InnerText;
        XmlNode it = xmlNode.FirstChild;
        while (it != null)
        {
            switch (it.Name)
            {
                case "decrease_on_turn_start":
                    decreaseOnTurnStart = int.Parse(it.InnerText);
                    break;
                case "decrease_on_turn_end":
                    decreaseOnTurnEnd = int.Parse(it.InnerText);
                    break;
                case "stackable":
                    stackable = bool.Parse(it.InnerText);
                    break;
                case "negative":
                    allowNegative = bool.Parse(it.InnerText);
                    break;
                case "on_turn_start":
                    OnTurnStart = SemanticAnalyser.AnalayseEffectList(it);
                    break;
                case "on_after_play_card":
                    OnAfterPlayCard = SemanticAnalyser.AnalayseEffectList(it);
                    break;
                case "on_add":
                    OnAdd = SemanticAnalyser.AnalayseEffectList(it);
                    break;
                case "on_remove":
                    OnRemove = SemanticAnalyser.AnalayseEffectList(it);
                    break;
                default:
                    throw new SemanticException();
            }
            it = it.NextSibling;
        }
    }
}

[Serializable]
public class StatusCounter
{
    public Status status = null;
    public int value = 0;
}