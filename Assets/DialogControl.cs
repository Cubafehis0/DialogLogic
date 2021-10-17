using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
public class DialogControl : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    
    public Story story;
    [SerializeField]
    private Canvas canvas = null;
   
    // UI Prefabs
    [SerializeField]
    private Text textPrefab = null;
    [SerializeField]
    private Canvas narrationPrefab = null;
    [SerializeField]
    private TextAsset inkJSONAsset = null;

    RoleImageControl roleImageControl;
    private void Awake()
    {
        string name, content;
        GetNameAndContent("男孩 ：  “你好，我是杰克。”",out name,out content);
        Debug.Log(name);
        Debug.Log(content);
        //RemoveChildren();
        //StartStory();
    }

    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    void RefreshView()
    {
        RemoveChildren();

        if(story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            string name, content;
        }
        //else if (story.currentChoices.Count > 0)
        //{
        //    for (int i = 0; i < story.currentChoices.Count; i++)
        //    {
        //        Choice choice = story.currentChoices[i];
        //        Button button = CreateChoiceView(choice.text.Trim());
        //        // Tell the button what to do when we press it
        //        button.onClick.AddListener(delegate
        //        {
        //            OnClickChoiceButton(choice);
        //        });
        //    }
        //}
        //// If we've read all the content and there's no choices, the story is finished!
        //else
        //{
        //    Button choice = CreateChoiceView("End of story.\nRestart?");
        //    choice.onClick.AddListener(delegate
        //    {
        //        StartStory();
        //    });
        //}
    }
    void OnClickNarration()
    {
        RefreshView();
    }
    bool GetNameAndContent(string s,out string name,out string content)
    {
        string nameParttern = @".+\s";
        string contentParttern = "“.+”";

        var nameMatch = Regex.Matches(s, nameParttern);
        Debug.Log(nameMatch.Count);
        if (nameMatch.Count == 1)
        {
            name = nameMatch[0].Value;
            name.Replace(':', ' ');
            name.Replace('：', ' ');
            name.Trim();
        }
        else
        {
            Debug.LogError("正则表达式不匹配");
            name = null;
            content = null;
            return false;
        }
        var contentMatch = Regex.Matches(s, contentParttern);
        if (contentMatch.Count == 1)
        {
            content = contentMatch[0].Value;
            content.Replace('“', ' ');
            content.Replace('”', ' ');
            content.Trim();
        }
        else
        {
            Debug.LogError("正则表达式不匹配");
            content = null;
            return false;
        }
        return true;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        roleImageControl=RoleImageControl.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateNarration(string speakerName,string content)
    {
        Sprite sprite = roleImageControl.GetSpriteByName(speakerName);
        if (sprite == null)
            Debug.LogError("无法找到对应角色的精灵");
        else
        {
            Image image = narrationPrefab.GetComponentInChildren<Image>();
            image.sprite = sprite;
        }
        var narration=Instantiate(narrationPrefab);
        narration.transform.SetParent(canvas.transform);
    }
    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }
}
