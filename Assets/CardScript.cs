using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    //[SerializeField] int damage;
    //[SerializeField] int shield;
    [SerializeField] Stack stack;
    [SerializeField]  GameObject button;
    [SerializeField]  GameObject panel;
    //GameObject clone;
    public void Start()
    {
        button = transform.parent.gameObject;
        //panel = button.transform.parent.gameObject;
        //stack = panel.transform.parent.GetComponent<Stack>();
    }
    public void DiscardCard()
    {
        Debug.Log("You Discarded the Card");
        stack.MoveCard(1,button);
        //clone = transform.parent.gameObject;
        //stack.Discard.Add(clone);
        //Destroy(transform.parent.gameObject);
    }
}
