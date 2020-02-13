using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public static PlayerStats instance;
    //our deck container which holds our starting deck, should be in a child object
    private DeckContainer theDeck;
    public BaseClassless playerClass = new BaseClassless();

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            theDeck = GetComponentInChildren<DeckContainer>();
            playerClass.deckList = theDeck.deck;
        }
        //I know technically speaking you don't need brackets for single lines but I like them because it helps me understand the code
        else if (instance != this)
        {
            Destroy(gameObject);
        }
            
    }

    CombatController combatController;

    public int posStatus = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CallStatus(1, posStatus, 3, 3);
            Debug.Log("pos" + posStatus);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CallStatus(2, posStatus, 3, 3);
            Debug.Log("pos" + posStatus);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CallStatus(3, posStatus, 3, 3);
            Debug.Log("pos" + posStatus);
        }
    }

    public void HookUpToCombatController()
    {
        combatController = FindObjectOfType<CombatController>();
    }

    // status is played during player turn
    /// <summary>
    /// create a status effect on the player
    /// </summary>
    /// <param name="whichStatus">the status you're applying, 1 = grit, 2 = accuracy, 3 = shield</param>
    /// <param name="pos">this is PlayerStats.instance.posStatus, probably shouldn't be a parameter</param>
    /// <param name="status">the amount you're applying</param>
    /// <param name="turns">the number of turns to apply it for</param>
    public void CallStatus(int whichStatus, int pos, int status, int turns)
    {
        // 1- grit, 2- accuracy, 3- shield
        switch (whichStatus)
        {
            case 1:
                combatController.playerTurnEvent.AddListener(delegate { GainGritForXTurns(pos, status, turns); });
                combatController.playerTurnEvent.Invoke();
                break;
            case 2:
                combatController.playerTurnEvent.AddListener(delegate { GainAccuracyForXTurns(pos, status, turns); });
                combatController.playerTurnEvent.Invoke();
                break;
            case 3:
                combatController.playerTurnEvent.AddListener(delegate { GainShieldForXTurns(pos, status, turns); });
                combatController.playerTurnEvent.Invoke();
                break;
            default:
                Debug.Log("PlayerStats- CallStatus() error");
                break;
        }

        combatController.playerTurnEvent.RemoveAllListeners();
        posStatus++;
    }

    public void GainGritForXTurns(int pos, int grit, int turns)
    {
        playerClass.currentGrit += grit;
        playerClass.status[pos].gritApplied = grit;
        playerClass.status[pos].numTurnsLeft = turns;
    }

    public void GainAccuracyForXTurns(int pos, int accuracy, int turns)
    {
        playerClass.accuracy += accuracy;
        playerClass.status[pos].accuracyApplied = accuracy;
        playerClass.status[pos].numTurnsLeft = turns;
    }

    public void GainShieldForXTurns(int pos, int shield, int turns)
    {
        playerClass.shield += shield;
        playerClass.status[pos].shieldApplied = shield;
        playerClass.status[pos].numTurnsLeft = turns;
    }

    public void DecrementStatus()
    {
        int count = 0;

        for (int i = 0; i < playerClass.status.Length; i++)
        {
            // reduce status turn counter
            if (playerClass.status[i].numTurnsLeft > 0)
            {
                playerClass.status[i].numTurnsLeft -= 1;

                // if status is over, revert status effect
                if (playerClass.status[i].numTurnsLeft <= 0)
                {
                    if (playerClass.status[i].gritApplied != 0)
                    {
                        playerClass.currentGrit -= playerClass.status[i].gritApplied;
                        playerClass.status[i].gritApplied = 0;
                    }
                    else if (playerClass.status[i].accuracyApplied != 0)
                    {
                        playerClass.accuracy -= playerClass.status[i].accuracyApplied;
                        playerClass.status[i].accuracyApplied = 0;
                    }
                    else if (playerClass.status[i].shieldApplied != 0)
                    {
                        playerClass.shield -= playerClass.status[i].shieldApplied;
                        playerClass.status[i].shieldApplied = 0;
                    }

                    playerClass.status[i].numTurnsLeft = 0;
                    posStatus--;
                    count++;
                }
            }
        }

        // update status stack
        UpdateStatusList(count);
    }

    public void UpdateStatusList(int count)
    {
        for (int i = 0; i < playerClass.status.Length; i++)
        {
            if (count > 0 && i - count >= 0)
            {
                playerClass.status[i - count].gritApplied = playerClass.status[i].gritApplied;
                playerClass.status[i - count].accuracyApplied = playerClass.status[i].accuracyApplied;
                playerClass.status[i - count].shieldApplied = playerClass.status[i].shieldApplied;
                playerClass.status[i - count].numTurnsLeft = playerClass.status[i].numTurnsLeft;

                playerClass.status[i].gritApplied = 0;
                playerClass.status[i].accuracyApplied = 0;
                playerClass.status[i].shieldApplied = 0;
                playerClass.status[i].numTurnsLeft = 0;
            }
        }
    }

    public void ResetStats()
    {
        playerClass.maxHealth = 30;
        playerClass.currentHealth = 30;

        playerClass.maxGrit = 4;
        playerClass.currentGrit = 4;
        playerClass.accuracy = 0;
        playerClass.shield = 0;
    }

    public void TakeDamage(int damageTaken)
    {
        playerClass.currentHealth -= damageTaken;
        //if we die, change state to loss state because we lost
        if (playerClass.currentHealth <= 0)
        {
            //find our combat controller (maybe should be using a singleton pattern for combat controller)
            CombatController combatController = FindObjectOfType<CombatController>();
            //change state to loss
            combatController.ChangeState(CombatState.Loss);
        }
        //if we healed over our max health, change health so it's back to max health
        else if (playerClass.currentHealth >= playerClass.maxHealth)
        {
            playerClass.currentHealth = playerClass.maxHealth;
        }
    }

    public void GameStateChanged(GameState newState)
    {
        if (newState == GameState.MainMenu)
        {
            playerClass.currentHealth = playerClass.maxHealth;
        }
    }
}
