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

    //None = 0, Root = 1, How to Play = 2, Credits = 3
    void OnStateChanged(MenuState newState)
    {
        int index = (int)newState - 1;
        //Debug.Log(index);
        DisablePanels();
        IEnumerator coroutine = panels[index].GetComponent<IPanel>().AnimateIn();
        StartCoroutine(coroutine);
    }

    void DisablePanels()
    {
        foreach (GameObject obj in panels)
        {
            IPanel panel = obj.GetComponent<IPanel>();
            if (panel.InScene())
            {
                IEnumerator coroutine = panel.AnimateOut();
                StartCoroutine(coroutine);
            }
        }
    }
}
