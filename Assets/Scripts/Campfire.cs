using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    PlayerStats playerStats;
    public int healAmount;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void HealPlayer()
    {
        Debug.Log(playerStats.playerClass.currentHealth);
        if (playerStats.playerClass.currentHealth < playerStats.playerClass.maxHealth)
        {
            if (playerStats.playerClass.currentHealth + healAmount <= playerStats.playerClass.maxHealth)
            {
                playerStats.playerClass.currentHealth += healAmount;
            }
            else
            {
                playerStats.playerClass.currentHealth = playerStats.playerClass.maxHealth;
            }
        }
        Debug.Log(playerStats.playerClass.currentHealth);
    }
}
