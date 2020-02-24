using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ModalPanel : MonoBehaviour
{
    public List<GameObject> thingsToDisable = new List<GameObject>();
    public static event Action<bool> OptionSelected = delegate { };
    [Tooltip("The tmp text that describes what the panel is for")]
    [SerializeField] TextMeshProUGUI heading;

    [Tooltip("The text that the heading starts with")]
    [TextArea(2,3)]
    [SerializeField] string headingText;

    // Start is called before the first frame update
    void Start()
    {
        SetText(headingText);
        if (thingsToDisable != null)
        {
            DisableThings();
        }
        AnimateIn();
    }

    void DisableThings()
    {
        foreach (GameObject obj in thingsToDisable)
        {
            obj.SetActive(false);
        }
    }

    void EnableThings()
    {
        foreach (GameObject obj in thingsToDisable)
        {
            obj.SetActive(true);
        }
    }

    public void AnimateIn()
    {
        //Play Animation
        //I'm thinking it probably slides up from off screen or fades in or something
    }

    public void AnimateOut()
    {
        //play animation to remove self from scene, maybe slide off screen?
        //disable self since we out
        this.enabled = false;
    }
    /// <summary>
    /// tell whoever's listening what the user decided,
    /// re-enable the stuff we disabled,
    /// and disable ourselves
    /// </summary>
    /// <param name="choice"></param>
    public void SelectOption(bool choice)
    {
        EnableThings();
        OptionSelected.Invoke(choice);
        AnimateOut();
    }

    public void SetText(string text)
    {
        heading.text = text;
        headingText = text;
    }
}
