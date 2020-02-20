using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseClassless 
{
    [Header("Essentials")]
    public int maxHealth;
    public int currentHealth;
    [ReadOnly]
    public GameObject[] deckList;

    [Header("Combat")]
    public int maxGrit;
    public int currentGrit;
    public int accuracy;
    public int shield;
    public List<Status> status = new List<Status>();

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
