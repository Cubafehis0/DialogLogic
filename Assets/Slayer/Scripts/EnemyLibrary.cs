using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StringPawnDictionary : SerializableDictionary<string, Pawn> { }

public class EnemyLibrary : Singleton<EnemyLibrary>
{
    [SerializeField]
    private StringPawnDictionary enemyLibrary = new StringPawnDictionary();

    public Pawn GetEnemy(string name)
    {
        return Instantiate(enemyLibrary[name]);
    }
}
