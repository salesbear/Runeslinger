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
    }
    public void PlayCard()
    {
        Debug.Log("You Played Card");
        Destroy(gameObject);
    }
    void OnMouseDown()
    {
        Debug.Log("You Played Card");
        Destroy(gameObject);
    }
}
