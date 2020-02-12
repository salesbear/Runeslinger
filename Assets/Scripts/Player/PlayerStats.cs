using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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

    public int posStatus = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = new BaseClassless();
        playerClass.deckList = Resources.LoadAll("CardAssets", typeof(Card));
    }

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
}
