using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject[] panels;

    private void OnEnable()
    {
        MainMenuController.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        MainMenuController.StateChanged -= OnStateChanged;
    }

    //None = 0, Root = 1, Settings = 2, Credits = 3
    void OnStateChanged(MenuState newState)
    {
        int index = (int)newState - 1;
        //Debug.Log(index);
        DisablePanels();
        panels[index].gameObject.SetActive(true);
    }

    void DisablePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.gameObject.SetActive(false);
        }
    }
}
