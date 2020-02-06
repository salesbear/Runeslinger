using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] List<GameObject> PossibleCards = new List<GameObject>();
    [SerializeField] List<int> Instances = new List<int>(); // Details the Instances of the Possible Card with the corresponding index
    [SerializeField] public Transform[] Placements = new Transform[6];
    public Stack<GameObject> Deck = new Stack<GameObject>();
    public List<GameObject> Discard = new List<GameObject>();
    public bool[] placements = new bool[6];
    int randNumber;

    private void Start()
    {
        for(int i = 0; i < PossibleCards.Count; i++)
        {
            for(int y = 0; y < Instances[i]; y++)
            {
                Discard.Add(PossibleCards[i]);
            }
        }
        if(Discard.Count < 12)
        {
            Debug.Log("Not Enough Cards");
        }
        if(Discard.Count > 12)
        {
            Debug.Log("Too Many Cards");
        }
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
        for (int i = 0; i < 6; i++)
        {
            Debug.Log("Popped");
            GameObject popped = Deck.Pop();
            Discard.Add(popped);
            Instantiate(popped, Placements[i].transform.position, Quaternion.identity);
        }
    }
}
