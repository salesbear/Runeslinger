using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardDisplay),typeof(CardBehavior))]
public class CardMove : MonoBehaviour
{
    CardDisplay theCard;
    CardBehavior m_cardBehavior;
    Stack deck;
    bool m_playable = false;
    EnemyController enemyController;
    RectTransform playArea;
    [SerializeField] RectTransform playerTransform;
    [ReadOnly]
    [SerializeField] List<RectTransform> enemyTransforms = new List<RectTransform>();

    private void Awake()
    {
        theCard = GetComponent<CardDisplay>();
        m_cardBehavior = GetComponent<CardBehavior>();
        deck = FindObjectOfType<Stack>();
        enemyController = FindObjectOfType<EnemyController>();
        //get the rectTransform from playArea
        playArea = GameObject.FindGameObjectWithTag("PlayArea").GetComponent<RectTransform>();
    }

    private void Start()
    {
        foreach (EnemyDisplay enemy in enemyController.enemies)
        {
            enemyTransforms.Add(enemy.enemyTransform);
        }
    }


    private void Update()
    {
        foreach (RectTransform transform in enemyTransforms)
        {
            if (transform.Overlaps(playerTransform, true))
            {
                m_cardBehavior.SetTarget(transform.gameObject);
            }
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log("MouseDown");
    }

    private void OnMouseDrag()
    {
        //Debug.Log("Mouse Drag");

        //point to move card to
        Vector3 point;
        //find mouse position
        Vector2 mousePos = new Vector2();
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        //set point based on mouse position
        point = new Vector3(mousePos.x, mousePos.y, 0);
        //get the world position based on where the mouse was
        point = Camera.main.ScreenToWorldPoint(point);
        //change only the x and y coordinates of the card based on where the mouse was
        gameObject.transform.position = new Vector3(point.x, point.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        if (playerTransform.Overlaps(playArea))
        {
            if (m_cardBehavior.HasTarget())
            {
                m_cardBehavior.PlayCard();
            }
            else
            {
                //if we're not playing it put it back in hand
                deck.ResetCard(gameObject);
            }
        }
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("TriggerEnter2D");
    //    if (collision.CompareTag("Enemy") && theCard.target == TargetingOption.Enemy)
    //    {
    //        m_cardBehavior.SetTarget(collision.gameObject);
    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy") && theCard.target == TargetingOption.Enemy)
    //    {
    //        m_cardBehavior.SetTarget(other.gameObject);
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("TriggerStay2D");
    //    if (collision.CompareTag("PlayArea"))
    //    {
    //        //get the collider from the play area
    //        BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>();
    //        //if we're at least half inside play area, let us play the card
    //        if (transform.position.y >= collider.bounds.min.y)
    //        {
    //            m_playable = true;
    //        }
    //        else
    //        {
    //            m_playable = false;
    //        }
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("TriggerStay");
    //    //check if we're in the play area, so we know if the player wants to play the card
    //    if (other.CompareTag("PlayArea"))
    //    {
    //        //should only be true if card is at least half inside the play area
    //        if (transform.position.y >= other.bounds.min.y)
    //        {
    //            m_playable = true;
    //        }
    //        else
    //        {
    //            m_playable = false;
    //        }
    //    }
    //}

}
