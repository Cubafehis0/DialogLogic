using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardEditor
{
    public class SwitchCardList : MonoBehaviour
    {
        [SerializeField]
        CardType category;
        public void Click()
        {
            Debug.Log("Click2SwitchCardList");
        }
    }

}
