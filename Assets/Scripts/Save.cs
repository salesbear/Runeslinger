using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    int playerHealth;
    GameObject[] deck;
    int roundsWon;

    public static Save CreateSave()
    {
        Save save = new Save();
        save.playerHealth = PlayerStats.instance.playerClass.currentHealth;
        save.deck = PlayerStats.instance.playerClass.deckList;
        save.roundsWon = 0;
        return save;
    }
}
