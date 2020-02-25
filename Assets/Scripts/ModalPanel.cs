using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ModalPanel : MonoBehaviour
{
    public List<GameObject> thingsToDisable = new List<GameObject>();
    public static event Action<bool> OptionSelected = delegate { };
    public static event Action AnimateOutEnded = delegate { };
    [Tooltip("The tmp text that describes what the panel is for")]
    [SerializeField] TextMeshProUGUI heading;

    [Tooltip("The text that the heading starts with")]
    [TextArea(2,3)]
    [SerializeField]
    string headingText;

    [Tooltip("the point in space where the panel stops moving")]
    [SerializeField]
    Transform endPoint;

    [Tooltip("ease controls how much you want to ease the animations")]
    [SerializeField]
    float ease = 0.5f;

    [Tooltip("How long it takes for the panel animation to happen")]
    [SerializeField]
    float lerpTime = 1f;
    float currentLerpTime = 0f;
    Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        SetText(headingText);
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

    private void OnDisable()
    {
        StopCoroutine("AnimateIn");
        StopCoroutine("AnimateOut");
    }

    public IEnumerator AnimateIn()
    {
        Debug.Log("Animate In called");
        DisableThings();
        //Play Animation
        //I'm thinking it probably slides up from off screen or fades in or something
        while (currentLerpTime < lerpTime)
        {
            //increase lerp time
            currentLerpTime += Time.deltaTime;
            //get the value for how far we are in the lerp
            float percentDone = currentLerpTime / lerpTime;
            //use a sin function to ease out
            percentDone = Mathf.Sin(percentDone * Mathf.PI * ease);
            //set our position to be the Lerp value
            transform.position = Vector3.Lerp(startPoint, endPoint.position, percentDone);
            yield return null;
        }
        currentLerpTime = 0f;
    }

    public IEnumerator AnimateOut()
    {
        //play animation to remove self from scene, maybe slide off screen?
        while (currentLerpTime < lerpTime)
        {
            //increase lerp time
            currentLerpTime += Time.deltaTime;
            //get the value for how far we are in the lerp
            float percentDone = currentLerpTime / lerpTime;
            //use a cos function to ease in as we go out
            percentDone = 1 - Mathf.Cos(percentDone * Mathf.PI * ease);
            //set our position to be the Lerp value
            transform.position = Vector3.Lerp(endPoint.position, startPoint, percentDone);
            yield return null;
        }
        currentLerpTime = 0f;
        AnimateOutEnded.Invoke();
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
        IEnumerator coroutine = AnimateOut();
        //check if the game object is active before calling it
        if (gameObject.activeSelf)
        {
            StartCoroutine(coroutine);
        }
    }

    public void SetText(string text)
    {
        heading.text = text;
        headingText = text;
    }
}
