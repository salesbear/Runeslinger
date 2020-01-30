using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuController : MonoBehaviour
{
    public MenuState state { get; private set; }
    public static event Action<MenuState> StateChanged = delegate { };

    private void Start()
    {
        ChangeState(MenuState.Root);
    }

    /// <summary>
    /// change to new state based on index, 1 = Root, 2 = Settings, 3 = Credits
    /// </summary>
    /// <param name="stateIndex">the index</param>
    public void ChangeState(int stateIndex)
    {
        //convert index to state type, 1 = Root, 2 = Settings, 3 = Credits
        state = (MenuState)stateIndex;
        StateChanged.Invoke(state);
    }

    /// <summary>
    /// change to specified new state
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(MenuState newState)
    {
        state = newState;
        StateChanged.Invoke(state);
    }
}
