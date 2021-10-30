using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink2Unity;
public class FunctionTest : MonoBehaviour
{
        
<<<<<<< HEAD
        public TextAsset asset;
        public int choose;
        public bool ctS;
        public bool waitC;
        InkUnity ink2Unity;
        public int[] data;
        // Start is called before the first frame update
        void Start()
        {
            data = new Player().data;
            ink2Unity = new InkUnity(asset);
            choose = -1;
            ctS = false;
            waitC = false;
=======
    public TextAsset asset;
    public int choose;
    public bool ctS;
    public bool waitC;
    Ink2Unity ink2Unity;
    public int[] data;
    // Start is called before the first frame update
    void Start()
    {
        data = CardGameManager.Instance.player.data;
        ink2Unity = new Ink2Unity(asset);
        choose = -1;
        ctS = false;
        waitC = false;
    }

    // Update is called once per frame
    void Update()
    {
        //是否在等候选择
        if(waitC )
        {
            //choose>0表示已经做出选择
            if(choose>=0)
            {
                Content c=ink2Unity.SelectChoice(choose);
                if (c != null)
                    //输出选择后玩家会说的内容
                    Debug.Log(c);
                waitC = false;
                choose = -1;
            }
>>>>>>> 62adfc089916a38617517fcccba5e4624f24082f
        }
        else if(ctS)
        {
            if (ink2Unity.CanCoutinue)
            {
                var c = ink2Unity.CurrentContent();
                Debug.Log(c);
            }
            else if(!ink2Unity.IsFinished)
            {
                var cs = ink2Unity.CurrentChoices();
                foreach(var c in cs)
                {
                    Debug.Log(c);
                }
                waitC = true;
            }
            else
            {
                Debug.Log("Finish");
            }
            ctS = false;
        }
    }
}
