using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
public class TalkControl : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public Role role;
    
    public Story story;
    [SerializeField]
    private Transform talk = null;
   
    // UI Prefabs
    [SerializeField]
    private GameObject contentPrefab = null;
    [SerializeField]
    private GameObject choicePrefab=null;
    [SerializeField]
    private Image roleImgae = null;
    [SerializeField]
    private TextAsset inkJSONAsset = null;

    RoleImageControl roleImageControl;

    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
        
    }

    void RefreshView()
    {
        RemoveChildren();
        Debug.Log(story.currentChoices.Count);
        if(story.canContinue)
        {
            string content = story.Continue();
            List<string> tags = story.currentTags;
            CreateTalkView(content,tags);
        }
        else if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim(),i);
                // Tell the button what to do when we press it
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?",0);
            choice.onClick.AddListener(delegate
            {
                StartStory();
            });
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        roleImageControl=RoleImageControl.GetInstance();
        RemoveChildren();
        StartStory();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    Button CreateChoiceView(string choiceContent,int index)
    {
        var choice = Instantiate(choicePrefab);
        choice.transform.SetParent(talk);
        Text choiceText = choice.GetComponentInChildren<Text>();
        string text;
        var tags=TagHandle.ChoiceCurrentTags(choiceContent, out text);
        choiceText.text = text;
        if(tags!=null)
        {
            foreach (var tag in tags)
            {
                string ppName;
                string value;
                TagHandle.GetPropertyNameAndValue(tag, out ppName, out value);
                switch (ppName)
                {
                    case "speaker":
                        SetRoleImage(value);
                        break;
                }
            }
        }
        return choice.GetComponent<Button>();
    }
    void SetRoleImage(string name)
    {
        Sprite sprite = roleImageControl.GetSpriteByName(name);
        if (sprite == null)
        {
            Debug.LogError("无法找到对应角色的精灵,使用默认精灵");
            roleImgae.sprite = roleImageControl.defaultSprite;
        }
        else
        {
            roleImgae.sprite = sprite;
        }
        return;
    }
    void CreateTalkView(string content,List<string> tags)
    {
        if(tags!=null)
        {
            foreach (var tag in tags)
            {
                string ppName;
                string value;
                TagHandle.GetPropertyNameAndValue(tag, out ppName, out value);
                switch (ppName)
                {
                    case "Speaker":
                        SetRoleImage(value);
                        break;
                }
            }
        }
        var contentObject=Instantiate(contentPrefab,talk.transform);
        Button button = contentObject.GetComponent<Button>();
        //Button button2 = talk.GetComponentInChildren<Button>();
        //Debug.Log(button1==button2);
        button .onClick.AddListener(OnClickNarration);
        contentObject.GetComponentInChildren<Text>().text = content;
    }
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }
    void OnClickNarration()
    {
        Debug.Log("click");
        RefreshView();
    }
    void RemoveChildren()
    {
        int childCount = talk.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            //延迟销毁
            GameObject.Destroy(talk.transform.GetChild(i).gameObject);
        }
    }
}
