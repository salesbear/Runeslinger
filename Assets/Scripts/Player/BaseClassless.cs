using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClassless : BaseCharacterClass
{
    public void Classless()
    {
        maxHealth = 30;
        currentHealth = 29;

        maxGrit = 4;
        currentGrit = 4;
        accuracyThisTurn = 0;
        accuracyThisBattle = 0;
        shield = 0;
    }
}
