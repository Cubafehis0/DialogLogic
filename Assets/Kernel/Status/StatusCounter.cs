using System;

[Serializable]
public class StatusCounter
{
    private Status status = null;
    private int value = 0;

    public Status Status { get => status; set => status = value; }
    public int Value { get => value; set => this.value = value; }

}