using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    T Request();
    void Recycle(T poolObject);
}
