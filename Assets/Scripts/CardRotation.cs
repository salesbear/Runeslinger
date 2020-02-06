using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to the card game object to display card`s rotation correctly.
/// </summary>

[ExecuteInEditMode]
public class CardRotation : MonoBehaviour {

    public RectTransform CardFront;
    public RectTransform CardBack;
    public Transform targetFacePoint;
    public Collider col;

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
            if (h.collider == col)
                passedThroughColliderOnCard = true;
        }

        if (passedThroughColliderOnCard != showingBack)
        {
            showingBack = passedThroughColliderOnCard;

            if (showingBack)
            {
                CardFront.gameObject.SetActive(false);
                CardBack.gameObject.SetActive(true);
            }
            else
            {
                CardFront.gameObject.SetActive(true);
                CardBack.gameObject.SetActive(false);
            }
        }
	}
}
