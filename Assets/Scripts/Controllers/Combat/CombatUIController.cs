using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIController : MonoBehaviour
{
    //first panel is root, second is loss, 3rd is victory, 4th is reward screen, 5th is view deck
    [SerializeField] GameObject[] panels;
    PauseUIController pauseController;

    private void Awake()
    {
        pauseController = FindObjectOfType<PauseUIController>();
    }

    private void OnEnable()
    {
        CombatController.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        CombatController.StateChanged -= OnStateChanged;
    }

    //TODO: Change this to work with an actual combat system with multiple panels active at once
    void OnStateChanged(CombatState newState)
    {
        switch ((int)newState)
        {
            //if we're in player turn
            case 1:
                DisablePanels();
                panels[0].SetActive(true);
                pauseController.AddPanel(panels[0]);
                break;
            //if we died
            case 4:
                DisablePanels();
                //set death panel active
                panels[1].SetActive(true);
                break;
            //if we're in win state
            case 5:
                DisablePanels();
                panels[2].SetActive(true);
                break;
            //if we beat the enemies, go to reward screen
            case 6:
                DisablePanels();
                panels[3].SetActive(true);
                break;
            //view deck panel
            case 7:
                DisablePanels();
                panels[4].SetActive(true);
                break;
            case 8:
                DisablePanels();
                panels[5].SetActive(true);
                break;
        }
    }

    //TODO: change this to work for an actual combat system with multiple panels active at once
    void DisablePanels()
    {
        foreach (GameObject panel in panels)
        {
            pauseController.RemovePanel(panel);
            panel.gameObject.SetActive(false);
        }
    }
}
