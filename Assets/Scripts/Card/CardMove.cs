using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardDisplay),typeof(CardBehavior))]
public class CardMove : MonoBehaviour
{
    CardDisplay theCard;
    CardBehavior m_cardBehavior;

    bool m_playable = false;

    private void Awake()
    {
        theCard = GetComponent<CardDisplay>();
        m_cardBehavior = GetComponent<CardBehavior>();
    }

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
    }

    private void OnMouseDrag()
    {
        Debug.Log("Mouse Drag");
        Vector3 point;
        Vector2 mousePos = new Vector2();
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        point = new Vector3(mousePos.x, mousePos.y, 0);
        point = Camera.main.ScreenToWorldPoint(point);
        gameObject.transform.position = new Vector3(point.x, point.y, transform.position.z);
        
    }

    private void OnMouseUp()
    {
        if (m_playable && m_cardBehavior.HasTarget())
        {
            m_cardBehavior.PlayCard();
        }
        else
        {
            //deck.ResetHand();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && theCard.target == TargetingOption.Enemy)
        {
            m_cardBehavior.SetTarget(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //check if we're in the play area, so we know if the player wants to play the card
        if (other.CompareTag("PlayArea"))
        {
            if (transform.position.y >= other.bounds.min.y)
            {
                m_playable = true;
            }
            else
            {
                m_playable = false;
            }
        }
    }
}
