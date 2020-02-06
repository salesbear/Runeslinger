using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] GameObject[] PossibleCards = new GameObject[6];
    [SerializeField] public Transform[] Placements = new Transform[6];
    public Stack<GameObject> Deck = new Stack<GameObject>();
    public List<GameObject> Discard = new List<GameObject>();
    int randNumber;

    private void Start()
    {
        Discard.Add(PossibleCards[0]);
        Discard.Add(PossibleCards[1]);
        Discard.Add(PossibleCards[2]);
        Discard.Add(PossibleCards[3]);
        Discard.Add(PossibleCards[4]);
        Discard.Add(PossibleCards[5]);
        Discard.Add(PossibleCards[0]);
        Discard.Add(PossibleCards[1]);
        Discard.Add(PossibleCards[2]);
        Discard.Add(PossibleCards[3]);
        Discard.Add(PossibleCards[4]);
        Discard.Add(PossibleCards[5]);
    }

    public void Shuffle()
    {
        for (int i = Discard.Count; i > 0; i--)
        {
            randNumber = Random.Range(0, i);
            Deck.Push(Discard[randNumber]);
            Discard.RemoveAt(randNumber);
        }
        Discard.Clear();
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
}
