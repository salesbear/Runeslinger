using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to the card game object to display card`s rotation correctly.
/// </summary>

[ExecuteInEditMode]
public class CardRotation : MonoBehaviour {

    public RectTransform cardFront;
    public RectTransform cardBack;
    public Transform targetFacePoint;
    public Collider collider;

    private bool showingBack = false;

	// Update is called once per frame
	void Update () 
    {
        // raycast from camera to target point on the face of the card
        // if it passes through the card`s collider first, show the back of the card
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.transform.position, (-Camera.main.transform.position + targetFacePoint.position).normalized, 
            (-Camera.main.transform.position + targetFacePoint.position).magnitude);

        bool passedThroughColliderOnCard = false;

        foreach (RaycastHit h in hits)
        {
            if (h.collider == collider)
                passedThroughColliderOnCard = true;
        }

        if (passedThroughColliderOnCard != showingBack)
        {
            showingBack = passedThroughColliderOnCard;

            if (showingBack)
            {
                cardFront.gameObject.SetActive(false);
                cardBack.gameObject.SetActive(true);
            }
            else
            {
                cardFront.gameObject.SetActive(true);
                cardBack.gameObject.SetActive(false);
            }
        }
	}
}
