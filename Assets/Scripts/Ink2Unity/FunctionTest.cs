using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{


    public class FunctionTest : MonoBehaviour
    {
        public bool conS;
        public bool wait;
        public int c;
        public bool save;
        public bool load;
        public string loadFile;
        public TextAsset textAsset;
        InkStory story;
        // Start is called before the first frame update
        void Start()
        {
            c = -1;
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
                Save.SaveGame(1,"axsa");
                save = false;
            }
            if (load)
            {
                ILoad l = new Load();
                l.LoadGame(1);
                load = false;
            }
            if (wait == true)
            {
                if(c>=0)
                {
                    story.SelectChoice(c);
                    wait = false;
                    c = -1;
                }
            }
            else if(conS)
            {
                if(story.CanCoutinue)
                {
                    Debug.Log(story.CurrentContent());
                }
                else if(story.CurrentChoicesCount>0)
                {
                    var cs = story.CurrentChoices();
                    foreach(var c in cs)
                    {
                        Debug.Log(c);
                    }
                    wait = true;
                }
                conS = false;
            }
        }
    }

}
