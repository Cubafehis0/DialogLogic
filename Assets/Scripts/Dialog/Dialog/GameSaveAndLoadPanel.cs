using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink2Unity;
public interface IGameSaveAndLoadPanel
{

}
public class GameSaveAndLoadPanel : MonoBehaviour, IGameSaveAndLoadPanel
{
    private GameObject gameSaveAndLoadPanel;
    [SerializeField]
    private InputField inputSaveInformation;
    [SerializeField]
    private Text inputText;
    [SerializeField]
    private GameObject saveButton;
    [SerializeField]
    private GameObject loadButton;
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject saveAndLoadButton1;
    [SerializeField]
    private GameObject saveAndLoadButton2;
    [SerializeField]
    private GameObject saveAndLoadButton3;
    private int index = 0;
    /// <summary>
    /// Save:True
    /// Load:False
    /// </summary>
    private bool isSaveOrLoad;

    private void Start()
    {
        gameSaveAndLoadPanel = this.gameObject;
        //inputSaveInformation = this.transform.Find("InputSaveInformation").GetComponent<InputField>();
        //inputText = inputSaveInformation.transform.Find("Text").GetComponent<Text>();
        inputSaveInformation.onEndEdit.AddListener(delegate { OnSubmit(); });
        closeButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickCloseButton(); });
        saveButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickSaveButton(); });
        loadButton.GetComponent<Button>().onClick.AddListener(delegate { OnClickLoadButton(); });
        saveAndLoadButton1.GetComponent<Button>().onClick.AddListener(delegate { OnClickSaveAndLoadButton(1); });
        saveAndLoadButton2.GetComponent<Button>().onClick.AddListener(delegate { OnClickSaveAndLoadButton(2); });
        saveAndLoadButton3.GetComponent<Button>().onClick.AddListener(delegate { OnClickSaveAndLoadButton(3); });

        gameSaveAndLoadPanel.SetActive(false);
    }


    private void OnClickSaveAndLoadButton(int Buttonindex)
    {
        index = Buttonindex;
        if (isSaveOrLoad)
        {
            inputSaveInformation.gameObject.SetActive(true);
        }
        else
        {
            Load load = new Load();
            load.LoadGame(index);
            gameSaveAndLoadPanel.SetActive(false);
        }
    }

    private void OnClickSaveButton()
    {
        isSaveOrLoad = true;
        gameSaveAndLoadPanel.SetActive(true);
        inputSaveInformation.gameObject.SetActive(false);
        inputSaveInformation.text = "";
        GetInformation();
    }
    private void OnClickLoadButton()
    {
        isSaveOrLoad = false;
        gameSaveAndLoadPanel.SetActive(true);
        inputSaveInformation.gameObject.SetActive(false);
        GetInformation();
    }
    private void OnSubmit()
    {
        Debug.Log(inputText.text);
        Save.SaveGame(index, inputText.text);
        inputSaveInformation.gameObject.SetActive(false);
        gameSaveAndLoadPanel.SetActive(false);
    }
    private void OnClickCloseButton()
    {
        gameSaveAndLoadPanel.SetActive(false);
    }

    private void GetInformation()
    {
        Load load = new Load();
        List<string> list = load.LoadMessage();
        if (list.Count > 1)
            saveAndLoadButton1.GetComponentInChildren<Text>().text = list[1];
        if (list.Count > 2)
            saveAndLoadButton2.GetComponentInChildren<Text>().text = list[2];
        if (list.Count > 3)
            saveAndLoadButton3.GetComponentInChildren<Text>().text = list[3];
    }
}
