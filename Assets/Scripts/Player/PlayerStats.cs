using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject injuredSound;
    public static PlayerStats instance;
    //our deck container which holds our starting deck, should be in a child object
    private DeckContainer theDeck;
    public BaseClassless playerClass = new BaseClassless();
    //the number of rounds the player has won
    [ReadOnly]
    public int roundsWon = 0;
    //The number of rounds since the player recieved a rare card option
    [ReadOnly]
    public int rarePityTimer = 0;
    //The number of rounds since the player received an uncommon card option
    [ReadOnly]
    public int uncommonPityTimer = 0;

    private PlayerUI playerUI;
    public GameObject takeDamageOverlay;
    public GameObject shieldOverlay;
    public GameObject healOverlay;

    AudioSource audioSource;
    public AudioClip takeDmgClip;

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

        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        GameStateController.StateChanged += GameStateChanged;
        CombatController.StateChanged += CombatStateChanged;
    }

    void OnDisable()
    {
        GameStateController.StateChanged -= GameStateChanged;
        CombatController.StateChanged -= CombatStateChanged;
    }

    CombatController combatController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerClass.status.Clear();
            playerClass.status.TrimExcess();
        }
    }

    public void HookUpToCombatController()
    {
        combatController = FindObjectOfType<CombatController>();
        playerUI = FindObjectOfType<PlayerUI>();
    }

    // status is played during player turn
    /// <summary>
    /// create a status effect on the player
    /// </summary>
    /// <param name="whichStatus">the status you're applying, 1 = grit, 2 = accuracy, 3 = shield</param>
    /// <param name="pos">this is PlayerStats.instance.posStatus, probably shouldn't be a parameter</param>
    /// <param name="status">the amount you're applying</param>
    /// <param name="turns">the number of turns to apply it for</param>
    public void CallStatus(int whichStatus, int status, int turns)
    {
        if (status != 0)
        {
            // 1- grit, 2- accuracy, 3- shield
            //this doesn't work super great because the combat controller isn't always in the same scene as the player, maybe try to use static events instead
            switch (whichStatus)
            {
                case 1:
                    combatController.playerTurnEvent.AddListener(delegate { GainGritForXTurns(status, turns); });
                    combatController.playerTurnEvent.Invoke();
                    break;
                case 2:
                    combatController.playerTurnEvent.AddListener(delegate { GainAccuracyForXTurns(status, turns); });
                    combatController.playerTurnEvent.Invoke();
                    break;
                case 3:
                    combatController.playerTurnEvent.AddListener(delegate { GainShieldForXTurns(status, turns); });
                    combatController.playerTurnEvent.Invoke();
                    break;
                default:
                    Debug.Log("PlayerStats- CallStatus() error");
                    break;
            }
        }
        combatController.playerTurnEvent.RemoveAllListeners();
    }

    // create grit status effect and add to status list
    public void GainGritForXTurns(int grit, int turns)
    {
        playerClass.currentGrit += grit;

        Status effect = new Status();
        effect.gritApplied = grit;
        effect.numTurnsLeft = turns;

        playerClass.status.Add(effect);

        // playerUI.PopGritText();
    }

    // create accuracy status effect and add to status list
    public void GainAccuracyForXTurns(int accuracy, int turns)
    {
        int tmpAccuracy = playerClass.accuracy;

        playerClass.accuracy += accuracy;

        Status effect = new Status();
        effect.accuracyApplied = accuracy;
        effect.numTurnsLeft = turns;

        playerClass.status.Add(effect);

        if (tmpAccuracy < playerClass.accuracy)
            playerUI.PopAccuracyText();
    }

    // create shield status effect and add to status list
    public void GainShieldForXTurns(int shield, int turns)
    {
        int tmpShield = playerClass.shield;

        playerClass.shield += shield;

        Status effect = new Status();
        effect.shieldApplied = shield;
        effect.numTurnsLeft = turns;

        playerClass.status.Add(effect);

        if (tmpShield < playerClass.shield)
            playerUI.PopShieldText();

        Instantiate(shieldOverlay);
    }

    // decrement status effect
    public void DecrementStatus()
    {
        List<Status> removeList = new List<Status>();
        for (int i = 0; i < playerClass.status.Count; i++)
        {
            //whoopsy I fucked up and made a bug
            playerClass.status[i].numTurnsLeft--;
            //Debug.Log("Status: " + i + "\n" + playerClass.status[i]);
            // if effect is over, revert effect changes on player
            if (playerClass.status[i].numTurnsLeft == 0)
            {
                //Debug.Log("Status " + i + " has 0 turns left");
                if (playerClass.shield - playerClass.status[i].shieldApplied > 0)
                    playerClass.shield -= playerClass.status[i].shieldApplied;
                else
                    playerClass.shield = 0;

                playerClass.accuracy -= playerClass.status[i].accuracyApplied;
                playerClass.currentGrit -= playerClass.status[i].gritApplied;

                //add statuses to list to remove them later, DO NOT REMOVE THEM HERE
                removeList.Add(playerClass.status[i]);

            }
        }
        //remove all the status effects we need to
        for (int i = 0; i < removeList.Count; i++)
        {
            playerClass.status.Remove(removeList[i]);
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

        // clear status effect list for next fight
        playerClass.status.Clear();
    }

    public void TakeDamage(int damageTaken)
    {
        int tmpHealth = playerClass.currentHealth;

        Debug.Log("Take Damage");
        if (damageTaken > playerClass.shield)
        {
            playerClass.currentHealth -= (damageTaken - playerClass.shield);
        }
        if (damageTaken > 0)
        {
            Instantiate(injuredSound);
            playerClass.shield -= damageTaken;
        }
        else
        {
            playerClass.currentHealth -= damageTaken;
        }
        if (playerClass.shield < 0)
        {
            playerClass.shield = 0;
        }
        //if we die, change state to loss state because we lost
        if (playerClass.currentHealth <= 0)
        {
            Debug.Log("Health less than 0");
            //change state to loss
            combatController.ChangeState(CombatState.Loss);
        }
        //if we healed over our max health, change health so it's back to max health
        else if (playerClass.currentHealth >= playerClass.maxHealth)
        {
            playerClass.currentHealth = playerClass.maxHealth;
        }

        // animate health text when player health is changed
        if (tmpHealth != playerClass.currentHealth)
            playerUI.PopHealthText();

        // show red screen flash when player loses health
        if (tmpHealth > playerClass.currentHealth)
        {
            Instantiate(takeDamageOverlay);
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(takeDmgClip);
        }
        else if (tmpHealth < playerClass.currentHealth)
        {
            Instantiate(healOverlay);
        }
    }

    public void GameStateChanged(GameState newState)
    {
        if (newState == GameState.MainMenu)
        {
            ResetStats();
        }
    }

    //what we have right now is super broken so I'm just brute forcing it so it works
    public void CombatStateChanged(CombatState state)
    {
        //okay this one is probably actually necessary
        if (state == CombatState.RemoveCard)
        {
            Debug.Log("Won the fight - PlayerStats");
            ClearStatus();
        }
    }

    public void ClearStatus()
    {
        playerClass.status.Clear();
        playerClass.status.TrimExcess();
        playerClass.currentGrit = 4;
        playerClass.shield = 0;
        playerClass.accuracy = 0;
        roundsWon++;
    }
}
