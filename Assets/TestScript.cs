using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TestScript : MonoBehaviour
{
    public void Fun()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
    }

    private void Update()
    {
        Fun();
    }
}
