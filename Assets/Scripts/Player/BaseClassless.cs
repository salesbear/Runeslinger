using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseClassless 
{
    [System.Serializable]
    public struct Status
    {
        public int gritApplied;
        public int accuracyApplied;
        public int shieldApplied;
        public int numTurnsLeft;
    }

    [Header("Essentials")]
    public int maxHealth;
    public int currentHealth;
    [HideInInspector]
    public GameObject[] deckList;

    [Header("Combat")]
    public int maxGrit;
    public int currentGrit;
    public int accuracy;
    public int shield;
    public Status[] status = new Status[1000];

    public BaseClassless()
    {
        maxHealth = 30;
        currentHealth = 30;

        maxGrit = 4;
        currentGrit = 4;
        accuracy = 0;
        shield = 0;
    }
}
