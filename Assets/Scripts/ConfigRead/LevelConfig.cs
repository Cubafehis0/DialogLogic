using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class LevelConfig:ScriptableObject
{
    public int[] playerCharacter=new int[4];
    public int maxCardNum;
    public int drawCardNum;
}
