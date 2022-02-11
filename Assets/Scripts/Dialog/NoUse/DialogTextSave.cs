using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Ink2Unity;
public class DialogSaveAndLoadPanel : MonoBehaviour
{
    private static DialogSaveAndLoadPanel instance = null;
    public static DialogSaveAndLoadPanel Instance{ get => instance; }
    struct DialogTextSave_Content
    {
        public string speaker;
        public string text;
    };
    [SerializeField]
    private GameObject controllButton;
    [SerializeField]
    private GameObject showHistoricalDialog;
    [SerializeField]
    private Transform content;
    [SerializeField] 
    private Text text;

    private string path;
    private List<DialogTextSave_Content> textList = new List<DialogTextSave_Content>();

    public void SaveTextToFile(Content content, bool isChoose = false)
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
        sw.WriteLine(isChoose ? "Choose" : content.speaker + "#" + content.richText);
        sw.Close();
        sw.Dispose();
    }


    private void Awake()
    {
        instance = this;
        path = Application.dataPath + "/Resources/TextSave/text.txt";
        //controllButton = GameObject.Find("Buttons").transform.Find("ButtonToControllShowDialogSaveAndLoadPanel").gameObject;
        //showHistoricalDialog = this.transform.Find("ShowHistoricalDialog").gameObject;
        //content = showHistoricalDialog.transform.Find("Viewport").Find("Content");
        controllButton.GetComponent<Button>().onClick.AddListener(OnClickControllButton);
        //HideChildren(this.gameObject);
        ClearTextFile(path);
        //gameObject.SetActive(false);
    }

    private Color ChooseColor(string speaker)
    {
        if (speaker == "NPC")
        {
            return Color.white;
        }
        else if (speaker == "Player")
        {
            return Color.green;
        }
        else if (speaker == "Dialogue")
        {
            return Color.black;

        }
        else if (speaker == "Choose")
        {
            return Color.grey;       
        }
        return Color.blue;
    }

    private void ClearTextFile(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite);
        fs.Close();
    }

    private void GetTextForFile(string path)
    {
        textList.Clear();
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
    private void CreateHistricalDialog(string file)
    {
        GetTextForFile(file);
        for (int i = 0; i < textList.Count; i++)
        {
            Text text_obj = Instantiate(text);
            text_obj.color = ChooseColor(textList[i].speaker);
            text_obj.transform.SetParent(content, false);
            text_obj.text = textList[i].text;
        }
    }
    private void RemoveChildren(GameObject parent)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            Destroy(parent.transform.GetChild(i).gameObject);
    }
    private void OnClickControllButton()
    {
        if (showHistoricalDialog.activeSelf)
        {
            RemoveChildren(content.gameObject);
            showHistoricalDialog.gameObject.SetActive(false);
        }
        else
        {
            showHistoricalDialog.gameObject.SetActive(true);
            CreateHistricalDialog(path);
        }
        DialogSystem.Pause();
    }
}
