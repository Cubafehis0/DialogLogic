using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FunctionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string content = "<size=30>Some <color=yellow>RICH</color> text</size>. ";
        TagHandle.GetPureText(content);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
