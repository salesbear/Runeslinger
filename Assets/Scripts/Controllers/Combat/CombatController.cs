using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class CombatController : MonoBehaviour
{
    public CombatState state { get; private set; }
    public static event Action<CombatState> StateChanged;

    public UnityEvent playerTurnEvent;
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerTurnEvent.AddListener(delegate { playerStats.GainGritForXTurns(1, 1, 3); });
        //playerTurnEvent.RemoveListener(delegate { playerStats.GainGritForXTurns(0, 1, 3); });

        ChangeState(CombatState.PlayerTurn);
    }

    /// <summary>
    /// change to new state based on index, 1 = PlayerTurn, 2 = EnemyTurn, 
    /// 3 = Discard, 4 = Loss, 5 = Victory, 6 = RewardScreen
    /// </summary>
    /// <param name="stateIndex">the index</param>
    public void ChangeState(int stateIndex)
    {
        //convert index to state type
        state = (CombatState)stateIndex;
        StateChanged.Invoke(state);

        if (stateIndex == 1)
            playerTurnEvent.Invoke();
    }

    /// <summary>
    /// change to specified new state
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(CombatState newState)
    {
        state = newState;
        StateChanged.Invoke(state);
    }
}
