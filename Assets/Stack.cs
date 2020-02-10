using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField] List<GameObject> PossibleCards = new List<GameObject>();
    [SerializeField] List<int> Instances = new List<int>(); // Details the Instances of the Possible Card with the corresponding index
    //[SerializeField] public Transform[] Placements = new Transform[6];
    //public Stack<GameObject> Deck = new Stack<GameObject>();
    //public List<GameObject> Discard = new List<GameObject>();
    //public List<GameObject> Hand = new List<GameObject>();
    [SerializeField] GameObject DiscardPile;
    [SerializeField] GameObject DeckPile;
    [SerializeField] GameObject HandPile;
    [SerializeField] Transform placement;
    private HorizontalLayoutGroup HandLayout;
    //public bool[] placements = new bool[6];

    private void Awake()
    {
        HandLayout = transform.GetChild(0).GetComponent<HorizontalLayoutGroup>();
    }
    private void Start()
    {
        // Creates starting deck
        for(int i = 0; i < PossibleCards.Count; i++)
        {
            for(int y = 0; y < Instances[i]; y++)
            {
                GameObject temp = PossibleCards[i];
                //Instantiate(temp);
                //temp.transform.SetParent(DiscardPile.transform, false);
                Instantiate(temp,placement.position,placement.rotation, DiscardPile.transform);
                //Discard.Add(temp);
            }
        }
        // Checks deck size
        if(DiscardPile.transform.childCount < 12)
        {
            Debug.Log("Not Enough Cards");
        }
        if(DiscardPile.transform.childCount > 12)
        {
            Debug.Log("Too Many Cards");
        }
    }

    public void Shuffle()
    {
        for (int i = DiscardPile.transform.childCount; i > 0; i--)
        {
            Debug.Log("Shuffled");
            int randNumber = Random.Range(0, i);
            //GameObject temp = Discard[randNumber];
            if(DiscardPile.transform.childCount > 0)
            {
                MoveCard(2, DiscardPile.transform.GetChild(randNumber).gameObject);
            }
            //Deck.Push(temp);
            //Discard.RemoveAt(randNumber);
        }
    }

    public void Draw()
    {
        if(HandPile.transform.childCount < 6)
        {
            //if(Deck.Count == 0)
            if (DeckPile.transform.childCount == 0)
            {
                Shuffle();
            }
            Debug.Log("Popped");
            //GameObject popped = DeckPile.transform.GetChild(0).gameObject;
            MoveCard(1, DeckPile.transform.GetChild(0).gameObject);
            //popped.transform.SetParent(gameObject.transform, false);
            //Instantiate(popped,transform.GetChild(0));
            //Instantiate(popped);
            /*
            for (int i = 0; i < 6; i++)
            {
                Debug.Log("Popped");
                GameObject popped = Deck.Pop();
                Discard.Add(popped);
                Instantiate(popped);
            }
            */
        }
    }

    public void MoveCard(int position, GameObject card)
    {
        //card.transform.SetParent(transform.GetChild(position));

        //card.transform.parent = GetComponentInChildren<Transform>(); //transform.GetChild(position);
        if (position == 0)
        {
            Debug.Log("put in Discard");
            card.transform.SetParent(DiscardPile.transform, false);
            //Hand.Add(card);
        }
        if (position == 1)
        {
            Debug.Log("put in Hand");
            card.transform.SetParent(HandPile.transform, false);
            //Discard.Add(card);
        }
        if (position == 2)
        {
            Debug.Log("put in Deck");
            card.transform.SetParent(DeckPile.transform, false);
            //Deck.Push(card);
        }
    }

    public void ResetHandLayout()
    {
        if(HandLayout.enabled == true)
        {
            HandLayout.enabled = false;
        }
        else if(HandLayout.enabled == false)
        {
            HandLayout.enabled = true;
        }
        Debug.Log("Hand Layout Reset");
    }
}
