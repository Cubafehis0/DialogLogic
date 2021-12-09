using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController
{
    private DialogController dialogController;    
    public void Start()
    {
        dialogController = GameObject.Find("Dialog").GetComponent<DialogController>();

    }

    public void Update()
    {
      
        //dialogController.update();
    }
}
