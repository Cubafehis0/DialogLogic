using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName ="CardInfoTable",menuName ="TableAsset/CardInfoTable")]
public class CardInfoTable : ScriptableObject
{
    public List<CardInfo> cardInfos;
}