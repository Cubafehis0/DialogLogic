
using CardGame.Recorder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardLogEntry
{
    private string name;
    private CardCategory cardCategory;
    private bool isActive;
    private int turn;
    private CardLogEntryEnum logType;

    public int Turn { get => turn; set => turn = value; }
    public CardLogEntryEnum LogType { get => logType; set => logType = value; }
    public CardCategory CardCategory { get => cardCategory; set => cardCategory = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public string Name { get => name; set => name = value; }
}
