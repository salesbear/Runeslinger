using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] GameObject card1;
    [SerializeField] GameObject card2;
    [SerializeField] GameObject card3;
    public int field;
    [SerializeField] public Transform[] Placements = new Transform[6];
    public Stack<GameObject> Deck = new Stack<GameObject>();
    public List<GameObject> Discard = new List<GameObject>();
    private List<GameObject> Hand = new List<GameObject>();
    //private bool[] placements = new bool[];
    int randNumber;

    private void Start()
    {
        Discard.Add(card1);
        Discard.Add(card2);
        Discard.Add(card3);
        Discard.Add(card1);
        Discard.Add(card2);
        Discard.Add(card3);
    }

    public void Shuffle()
    {
        for (int i = Discard.Count; i > 0; i--)
        {
            randNumber = Random.Range(0, i);
            Deck.Push(Discard[randNumber]);
            Discard.RemoveAt(randNumber);
        }
    }

    public void Draw()
    {
        if (Deck.Count == 0)
        {
            Shuffle();
        }
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("Popped");
            GameObject popped = Deck.Pop();
            Discard.Add(popped);
            Instantiate(popped, Placements[i].transform.position, Quaternion.identity);
        }
    }

    public void AddToDiscard(GameObject played)
    {
        Discard.Add(played);
        Hand.Remove(played);
    }

    public void AddToHand(GameObject card)
    {
        Hand.Add(card);
    }
}
