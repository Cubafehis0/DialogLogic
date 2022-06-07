using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRelicLib
{
    List<Relic> RandomChooseRelics(int grade, Personality personality);
    void AddRelic(Relic relic);
    void RemoveRelic(Relic relic);
}
