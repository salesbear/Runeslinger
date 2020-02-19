using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int playerHealth { get; private set; }
    public GameObject[] deck { get; private set; }
    public int roundsWon { get; private set; }
    public int rarePity { get; private set; }
    public int uncommonPity { get; private set; }
    public static Save CreateSave()
    {
        Save save = new Save();
        save.playerHealth = PlayerStats.instance.playerClass.currentHealth;
        save.deck = PlayerStats.instance.playerClass.deckList;
        save.roundsWon = PlayerStats.instance.roundsWon;
        save.rarePity = PlayerStats.instance.rarePityTimer;
        save.uncommonPity = PlayerStats.instance.uncommonPityTimer;
        return save;
    }
}
