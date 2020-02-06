using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int shield;
    [SerializeField] PlayerScript player;
    [SerializeField] Stack stack;

    public void Start()
    {
        stack.AddToHand(gameObject);
    }
    public void PlayCard()
    {
        Debug.Log("You Played Card");
        stack.AddToDiscard(gameObject);
        Destroy(gameObject);
    }
    void OnMouseDown()
    {
        Debug.Log("You Played Card");
        stack.AddToDiscard(gameObject);
        Destroy(gameObject);
    }

    public void Move(Transform destination)
    {
        transform.position = destination.position;
    }
    private void Update()
    {
    }
}
