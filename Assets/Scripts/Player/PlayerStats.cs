using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    public BaseClassless playerClass;

    CombatController combatController;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = new BaseClassless();
    }

    private void Update()
    {
        Debug.Log(playerClass.currentHealth);
    }

    public void GainGritForXTurns(int pos, int gritGain, int turns)
    {

    }

    public void GainAccuracyForXTurns(int gritGain, int turns)
    {
        Debug.Log("Hi");
    }

    public void ResetStats()
    {
        playerClass.maxHealth = 30;
        playerClass.currentHealth = 30;

        playerClass.maxGrit = 4;
        playerClass.currentGrit = 4;
        playerClass.accuracyThisTurn = 0;
        playerClass.accuracyThisBattle = 0;
        playerClass.shield = 0;
    }
}
