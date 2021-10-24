using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class CardTable : ScriptableObject
{
	public List<CardEntity> Sheet1; // Replace 'EntityType' to an actual type that is serializable.
}
