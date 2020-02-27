using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status 
{
    public int gritApplied;
    public int accuracyApplied;
    public int shieldApplied;
    public int numTurnsLeft;

    public override string ToString()
    {
        return "grit applied: " + gritApplied
            + "\naccuracy applied: " + accuracyApplied
            + "\nshield applied: " + shieldApplied
            + "\nnum turns left: " + numTurnsLeft;
    }
}
