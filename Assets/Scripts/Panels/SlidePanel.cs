using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePanel : MonoBehaviour, IPanel
{
    [Tooltip("the point in space where the panel stops moving")]
    [SerializeField]
    Transform endPoint;
    [Tooltip("ease controls how much you want to ease the animations")]
    [SerializeField]
    float ease = 0.5f;

    [Tooltip("How long it takes for the panel animation to happen")]
    [SerializeField]
    float lerpTime = 0.5f;
    float currentLerpTime = 0f;
    [SerializeField]
    [ReadOnly]
    Vector3 startPoint;

    void Awake()
    {
        startPoint = transform.position;
    }

    public IEnumerator AnimateIn()
    {
        Debug.Log("Animate In called on SlidePanel");
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
        Debug.Log("Animate Out called in Slide Panel");
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
    }
}
