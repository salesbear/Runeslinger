using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIController : MonoBehaviour
{
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
        int index = (int)newState - 1;
        //Debug.Log(index);
        DisablePanels();
        panels[index].gameObject.SetActive(true);
        pauseController.AddPanel(panels[index]);
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
