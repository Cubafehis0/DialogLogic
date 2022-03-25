using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{
    public class FunctionTest : MonoBehaviour
    {
        public bool conS;
        public bool wait;
        public int c = -1;
        public bool save;
        public bool load;
        public string loadFile;
        public TextAsset textAsset;
        InkStory story;
        public Card card;
        // Start is called before the first frame update
        void Awake()
        {
            //Debug.Log(card.ConditionDesc);
            //TagHandle.ParseArray("(0,1,1,2)");
            story = new InkStory(textAsset);
            wait = false;
            conS = false;
            save = false; 
        }
        // Update is called once per frame
        void Update()
        {
            if (save)
            {
                SaveAndLoad.SaveGame(1,"axsa");
                save = false;
            }
            if (load)
            {
                SaveAndLoad.Load(1);
            }
            if (wait == true)
            {
                if(c>=0)
                {
                    story.SelectChoice(c,true);
                    wait = false;
                    c = -1;
                }
            }
            if(conS)
            {
                switch (story.NextState)
                {
                    case InkState.Init:
                        break;
                    case InkState.Content:
                        story.NextContent();
                        Debug.Log(story.CurrentContent());
                        break;
                    case InkState.Choice:
                        var cs = story.CurrentChoices();
                        foreach (var c in cs)
                        {
                            Debug.Log(c);
                        }
                        wait = true;
                        break;
                    case InkState.Finish:
                        Destroy(this);
                        break;
                    default:
                        break;
                }
                conS = false;
            }
        }
    }

}
