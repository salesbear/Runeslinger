using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject[] panels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        MainMenuController.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        MainMenuController.StateChanged -= OnStateChanged;
    }

    void OnStateChanged(MenuState newState)
    {
        int index = (int)newState - 1;
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
