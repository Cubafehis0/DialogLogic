using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class CardTable : ScriptableObject
{
    public List<CardEntity> cardpool_lgc;
    public List<CardEntity> cardpool_spt;
    public List<CardEntity> cardpool_mrl;
    public List<CardEntity> cardpool_imm;
    public List<CardEntity> cardpool_rdb;
    public List<CardEntity> cardpool_ags;
}
