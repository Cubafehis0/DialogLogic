using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ŀǰ����Ϊ���
/// </summary>
[RequireComponent(typeof(Deck))]
[RequireComponent(typeof(CardSpawnPoint))]
public class DrawDeck : MonoBehaviour
{
    public static DrawDeck instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
}
