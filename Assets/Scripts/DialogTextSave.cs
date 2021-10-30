using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Ink2Unity;
public class DialogTextSave : MonoBehaviour
{
    struct DialogTextSave_Content
    {
        public string speaker;
        public string text;
    };
    //public DialogTextLoad m_dialogTextLoad;
    private GameObject controll_Button;
    private GameObject showHistoricalDailog;
    public Text text;
    private Transform context;

    public string textSaveFile;

    private List<DialogTextSave_Content> textList = new List<DialogTextSave_Content>();
    private int index;
    
    private bool isActive = false;

    void Start()
    {
        controll_Button = this.transform.Find("ButtonToControllShowHistoricalDialog").gameObject;
        showHistoricalDailog = this.transform.Find("ShowHistoricalDialog").gameObject;
        context = showHistoricalDailog.transform.Find("Viewport").Find("Content");


        showHistoricalDailog.SetActive(false);
        if (controll_Button)
        {
            controll_Button.GetComponent<Button>().onClick.AddListener(delegate { ShowHistoricalDailog(); });
        }


        //开始清空文件
        textSaveFile = Application.dataPath + "/Resources/TextSave/text.txt";
        ClearTextFile(textSaveFile);
        //if (panel)
        //{
        //    Debug.Log("no panel");
        //}
        //m_dialogTextLoad = GameObject.Find("Canvas").transform.GetComponentInChildren<DialogTextLoad>();
    }

    //按钮控制历史对话显示
    private void ShowHistoricalDailog()
    {
        if (isActive)
        {
            RemoveChildren(context.gameObject);
            showHistoricalDailog.SetActive(false);
            isActive = false;
        }
        else
        {
            isActive = true;
            CreateHistricalDialog(textSaveFile);
            showHistoricalDailog.SetActive(true);
        }
    }
    private Color ChooseColor(string speaker)
    {
        if (speaker == "NPC")
        {
            return Color.white;
        }
        else if (speaker == "Lead")
        {
            return Color.green;
        }
        else if (speaker == "Dialogue")
        {
            return Color.black;

        }
        return Color.blue;
    }
    private void CreateHistricalDialog(string file)
    {
        GetTextForFile(file);
        for (int i = 0; i < textList.Count; i++)
        {
            Text text_obj = Instantiate(text);
            text_obj.color = ChooseColor(textList[i].speaker);
            text_obj.transform.SetParent(context, false);
            text_obj.text = textList[i].text;
        }
    }
    public void RemoveChildren(GameObject parent)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            Destroy(parent.transform.GetChild(i).gameObject);
    }
    public void GetTextForFile(string path)
    {
        textList.Clear();
        index = 0;
        string[] list = File.ReadAllLines(path);
        foreach (var line in list)
        {
            string[] texts = line.Split('#');

            if (texts.Length != 2) continue;

            DialogTextSave_Content content;
            content.speaker = texts[0];
            content.text = texts[1];
            textList.Add(content);

        }
        //Debug.Log(textList.Count);
    }

    public void SaveTextToFile(string path,Content content)
    {
        //Debug.Log("Savetext");
        //Debug.Log(path);
        StreamWriter sw;
        FileInfo file = new FileInfo(path);
        if (!File.Exists(path))
        {
            sw = file.CreateText();
        }
        else
        {
            sw = file.AppendText();
        }
        sw.WriteLine(content.speaker + "#" + content.richText);
        sw.Close();
        sw.Dispose();
    }
    public void ClearTextFile(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite);
        fs.Close();
    }
}
