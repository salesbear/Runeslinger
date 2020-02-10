using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    PlayerStats playerStats;
    BaseClassless player;
    public int healAmount;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        player = playerStats.playerClass;
    }

    public void HealPlayer()
    {
        if (player.currentHealth < player.maxHealth)
        {
            if (player.currentHealth + healAmount <= player.maxHealth)
                player.currentHealth += healAmount;
            else
                player.currentHealth = player.maxHealth;
        }
    }
}
