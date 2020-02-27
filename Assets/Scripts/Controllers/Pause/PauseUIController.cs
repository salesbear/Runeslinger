using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseUIController : MonoBehaviour
{
    [Tooltip("The Pause UI + all the panels in the scene disabled by pausing. The Pause UI should be your first panel")]
    [SerializeField] GameObject[] panels;
    List<GameObject> thePanels = new List<GameObject>();

    [SerializeField]
    ModalPanel modalPanel;

    GameStateController game;

    bool playerConfirmed = false;
    private void Awake()
    {
        game = FindObjectOfType<GameStateController>();
        foreach (GameObject panel in panels)
        {
            thePanels.Add(panel);
            //Debug.Log(panel);
            //Debug.Log(thePanels.ToArray().Length);
        }
    }

    private void OnEnable()
    {
        Debug.Log("Pause Enabled");
        GameStateController.StateChanged += OnPauseToggle;
        ModalPanel.OptionSelected += GetPlayerConfirmation;
        ModalPanel.AnimateOutEnded += ReturnToMainMenu;
    }

    private void OnDisable()
    {
        Debug.Log("Pause Disabled");
        GameStateController.StateChanged -= OnPauseToggle;
        ModalPanel.OptionSelected -= GetPlayerConfirmation;
        ModalPanel.AnimateOutEnded -= ReturnToMainMenu;
    }

    
    void OnPauseToggle(GameState newState)
    {
        TogglePause(newState == GameState.Pause);
    }

    void TogglePause(bool paused)
    {
        //Debug.Log(thePanels.ToArray().Length);
        //set the pause panel to be active if paused, inactive if not
        panels[0].SetActive(paused);
        //go through the non-pause panels and make them active if unpaused, inactive if paused
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(!paused);
        }
    }

    public void AddPanel(GameObject panel)
    {
        thePanels.Add(panel);
    }

    public void RemovePanel(GameObject panel)
    {
        if (thePanels.Contains(panel))
        {
            thePanels.Remove(panel);
        }
    }

    public void Dummy()
    {
        Debug.Log("Dummy");
    }

    public void GetPlayerConfirmation(bool choice)
    {
        if (choice && game.state == GameState.Pause)
        {
            playerConfirmed = true;
        } 
    }

    public void GetPlayerConfirmation()
    {
        IEnumerator coroutine = modalPanel.AnimateIn();
        modalPanel.StartCoroutine(coroutine);
    }

    void ReturnToMainMenu()
    {
        if (playerConfirmed)
        {
            game.ChangeState(GameState.MainMenu);
        }
    }
}
