using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class CombatUIController : MonoBehaviour
{
    //first panel is root, second is loss, 3rd is victory, 4th is reward screen, 5th is view deck
    [SerializeField] GameObject[] panels;
    PauseUIController pauseController;
    CombatController combatController;
    public static event Action CelebrationDone = delegate { };

    private void Awake()
    {
        pauseController = FindObjectOfType<PauseUIController>();
        combatController = FindObjectOfType<CombatController>();
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
                StartCoroutine(GetFked());
                break;
            //if we're in win state
            case 5:
                DisablePanels();
                panels[2].SetActive(true);
                break;
            //if we beat the enemies, go to reward screen
            case 6:
                StartCoroutine(CelebrateBeforeNextFight());
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

    IEnumerator CelebrateBeforeNextFight()
    {
        GameObject rootPanel = transform.GetChild(0).gameObject;
        Button[] buttons;

        buttons = rootPanel.GetComponentsInChildren<Button>();

        // disable buttons on win
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }

        yield return new WaitForSeconds(3);

        // disable combat panel
        DisablePanels();

        // set reward panel active
        panels[3].SetActive(true);

        // reenable buttons for next fight
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
        CelebrationDone.Invoke();
    }

    IEnumerator GetFked()
    {
        GameObject rootPanel = transform.GetChild(0).gameObject;
        Button[] buttons;

        buttons = rootPanel.GetComponentsInChildren<Button>();
        
        // disable buttons on death
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }

        // add lose feedback 
            // death sfx
            // screen fade dark

        yield return new WaitForSeconds(3);

        //set death panel active
        panels[1].SetActive(true);

        // show score
        HighScore highScore = FindObjectOfType<HighScore>();
        highScore.UpdateHighScoreText();

        // reenable buttons for next fight
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }
}
