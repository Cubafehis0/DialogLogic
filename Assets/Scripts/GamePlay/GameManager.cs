using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using System.Xml;
using System.Reflection;
using System;
using System.Xml.Serialization;
using XmlParser;
using System.IO;
using SemanticTree.PlayerEffect;
using SemanticTree;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //LoadXMLSettings();
        //CardGameManager.Instance.StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void LoadXMLSettings()
    {
        XmlDocument dom = new XmlDocument();
        TextAsset txt = Resources.Load("test") as TextAsset;
        dom.LoadXml(txt.text);
        XmlNode it = dom["root"].FirstChild;
        while (it != null)
        {
            switch (it.Name)
            {
                case "define_card":
                    StaticCardLibrary.Instance.DeclareCard(it);
                    break;
                case "define_status":
                    StaticStatusLibrary.DeclareStatus(it);
                    break;
                case "define_cost_modifier":
                    StaticCostModifierLibrary.Declare(it);
                    break;
            }
            it = it.NextSibling;
        }
        it = dom["root"].FirstChild;
        while (it != null)
        {
            switch (it.Name)
            {
                case "define_card":
                    StaticCardLibrary.Instance.DefineCard(it);
                    break;
                case "define_status":
                    StaticStatusLibrary.DefineStatus(it);
                    break;
                case "define_cost_modifier":
                    StaticCostModifierLibrary.Define(it);
                    break;
            }
            it = it.NextSibling;
        }
    }

}