using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameStateController : MonoBehaviour
{
    public GameState state { get; private set; }
    public static event Action<GameState> StateChanged = delegate { };
    //the state that the game starts at
    public GameState m_startingState;
    //the previous state, used for unpausing
    public GameState m_previousState { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(m_startingState);
    }

    /// <summary>
    /// change to new state based on index, 1 = MainMenu, 2 = LevelSelect, 3 = Pause, 
    /// 4 = Campsite, 5 = Combat, ResultScreen = 6
    /// </summary>
    /// <param name="stateIndex">the index</param>
    public void ChangeState(int stateIndex)
    {
        //store previous state for unpausing
        m_previousState = state;
        //convert index to state type
        state = (GameState)stateIndex;
        StateChanged.Invoke(state);
    }

    /// <summary>
    /// change to specified new state
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(GameState newState)
    {
        //Debug.Log("In ChangeState");
        //store previous state for unpausing
        m_previousState = state;
        state = newState;
        StateChanged.Invoke(state);
    }

    /// <summary>
    /// go back to the last state we had
    /// </summary>
    public void RevertToLastState()
    {
        ChangeState(m_previousState);
    }
}
