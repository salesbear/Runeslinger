using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int playerHealth { get; private set; }
    public string[] deck { get; private set; }
    public int roundsWon { get; private set; }
    public int rarePity { get; private set; }
    public int uncommonPity { get; private set; }
    
    public static Save CreateSave()
    {
        Save save = new Save();
        save.playerHealth = PlayerStats.instance.playerClass.currentHealth;
        save.roundsWon = PlayerStats.instance.roundsWon;
        save.rarePity = PlayerStats.instance.rarePityTimer;
        save.uncommonPity = PlayerStats.instance.uncommonPityTimer;
        save.deck = new string[12];
        for (int i = 0; i < PlayerStats.instance.playerClass.deckList.Length; i++)
        {
            save.deck[i] = PlayerStats.instance.playerClass.deckList[i].GetComponent<CardDisplay>().card.ToString();
        }
        return save;
    }
}
