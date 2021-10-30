using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    UIController m_UIController = new UIController();

    public void Start()
    {
        DontDestroyOnLoad(this);
        m_UIController.Start();
    }

    public void Update()
    {
        m_UIController.Update();

    }
}
