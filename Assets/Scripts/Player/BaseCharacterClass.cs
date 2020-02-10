using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterClass
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
}
