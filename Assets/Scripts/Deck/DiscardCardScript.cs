using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardScript : MonoBehaviour
{
    [SerializeField] Stack stack;
    public void DiscardCard()
    {
        Debug.Log("You Discarded a Card");
        stack.MoveCard(1,gameObject);
    }
}
