using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisuals
{
    void UpdateVisuals();

    int OrderInLayer { get; set; }
}