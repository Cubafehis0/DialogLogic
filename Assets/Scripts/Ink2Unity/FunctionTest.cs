using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Ink2Unity
{
    
    public class FunctionTest : MonoBehaviour
    {
        public TextAsset asset;
        // Start is called before the first frame update
        void Start()
        {
            Ink2Unity ink2Unity = new Ink2Unity(asset);
            Debug.Log(ink2Unity.CurrentContent().richText);
            Debug.Log(ink2Unity.CanCoutinue);
            //Debug.Log(ink2Unity.CurrentContent().richText);
            Debug.Log(ink2Unity.CurrentContent().richText.Length);
            Debug.Log(ink2Unity.CanCoutinue);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}