using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ink2Unity
{
    [System.Serializable]
    public partial class FunctionTest : MonoBehaviour
    {
        [SerializeField]
        int a = 0;
        [SerializeField]
        Personality personality;
        [SerializeField]
        bool save = false;
        [SerializeField]
        bool load = false;
        void Awake()
        {
            a = 1;
            personality = new Personality(1, 2, 3, 4);
        }
        private void Update()
        {
            if (save)
            {
                SaveAndLoad.SaveGame(0);
                save = false;
            }
            if (load)
            {
                SaveAndLoad.Load(0);
                load = false;
            }
        }
    }

}
