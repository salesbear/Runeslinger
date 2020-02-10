using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseClassless 
{
    [Header("Essentials")]
    public int maxHealth;
    public int currentHealth;
    public Card[] deckList;

    [Header("Combat")]
    public int maxGrit;
    public int currentGrit;
    public int accuracyThisTurn;
    public int accuracyThisBattle;
    public int shield;

    public BaseClassless()
    {
        maxHealth = 30;
        currentHealth = 30;

        maxGrit = 4;
        currentGrit = 4;
        accuracyThisTurn = 0;
        accuracyThisBattle = 0;
        shield = 0;
    }
}
