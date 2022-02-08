using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLibrary : MonoBehaviour
{
    private static readonly int[] neutralCharacter = new int[4] { 0, 0, 0, 0 };
    public static int[] NeutralCharacter
    {
        get => neutralCharacter.Clone() as int[];
    }
}
