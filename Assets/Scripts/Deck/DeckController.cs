using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Stack))]
public class DeckController : MonoBehaviour
{
    private Stack theDeck;
    public static event Action<int> DiscardDone = delegate { };
    private CombatController combatController;
    private void Awake()
    {
        theDeck = GetComponent<Stack>();
        combatController = FindObjectOfType<CombatController>();
    }

    private void Start()
    {
        theDeck.DrawCards(6);
    }

    //subscribe to CombatState changes when enabled
    private void OnEnable()
    {
        CombatController.StateChanged += OnStateChange;
    }
    //unsubscribe when disabled
    private void OnDisable()
    {
        CombatController.StateChanged -= OnStateChange;
    }

    void OnStateChange(CombatState state)
    {
        //on player turn, draw a new hand
        if (state == CombatState.PlayerTurn && (combatController.priorState == CombatState.Discard))
        {
            
            theDeck.DrawCards(6);
        }
        //on discard state, discard our hand
        if (state == CombatState.Discard)
        {
            theDeck.DiscardHand();
            //tell the combat controller to change state to player turn
            DiscardDone.Invoke(1);
        }
    }
    
}
