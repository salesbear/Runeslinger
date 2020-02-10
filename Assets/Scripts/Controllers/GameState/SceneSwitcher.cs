using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Tooltip("The states that cause a scene change")]
    [SerializeField] GameState[] statesToWatch;

    //we could do this based off of build index, but then we have to make sure we have the scenes built right
    [Tooltip("The corresponding scene names for the states")]
    [SerializeField] string[] sceneNames;
    //states that cause us to change levels
    private Dictionary<GameState, string> states = new Dictionary<GameState, string>();

    private void Start()
    {
        for (int i = 0; i < statesToWatch.Length; i++)
        {
            states.Add(statesToWatch[i], sceneNames[i]);
        }
    }

    private void OnEnable()
    {
        GameStateController.StateChanged += SwitchScene;
    }

    private void OnDisable()
    {
        GameStateController.StateChanged -= SwitchScene;
    }

    void SwitchScene(GameState newState)
    {
        
        if (states.ContainsKey(newState))
        {
            if (SceneManager.GetActiveScene().name != states[newState])
            {
                //Debug.Log("In Switch Scene");
                //Debug.Log(states);
                SceneManager.LoadScene(states[newState]);
            }
        }
    }
}
