using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int playerHealth { get; private set; }
    public GameObject[] deck { get; private set; }
    public int roundsWon { get; private set; }

    public static Save CreateSave()
    {
        Save save = new Save();
        save.playerHealth = PlayerStats.instance.playerClass.currentHealth;
        save.deck = PlayerStats.instance.playerClass.deckList;
        save.roundsWon = PlayerStats.instance.roundsWon;
        return save;
    }
}
