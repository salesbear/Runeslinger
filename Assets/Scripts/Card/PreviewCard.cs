using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCard : MonoBehaviour
{
    public float enlargeX = 25;
    public float enlargeY = 25;
    public float origXScale;
    public float origYScale;

    CombatController combatController;

    private void Start()
    {
        combatController = FindObjectOfType<CombatController>();
        CardMove cardMove = GetComponent<CardMove>();

        origXScale = transform.localScale.x;
        origYScale = transform.localScale.y;

        if (combatController.state == CombatState.ViewDeck || combatController.state == CombatState.ViewDiscard)
            cardMove.enabled = false;
        else
            cardMove.enabled = true;
    }

    public void OnMouseEnter()
    {
        transform.localScale += new Vector3(enlargeX, enlargeY, 1);
    }


    public void OnMouseExit()
    {
        // assuming you want it to return to its original size when your mouse leaves it.
        transform.localScale = new Vector3(origXScale, origYScale, 1);
    }
}
