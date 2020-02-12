using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField] List<GameObject> PossibleCards = new List<GameObject>();
    [SerializeField] List<int> Instances = new List<int>();
    [SerializeField] GameObject DeckPile;
    [SerializeField] GameObject DiscardPile;
    [SerializeField] GameObject HandPile;
    [SerializeField] Transform placement;
    private HorizontalLayoutGroup HandLayout;

    private void Awake()
    {
        // Get the hand layout
        HandLayout = GetComponentInChildren<HorizontalLayoutGroup>();
        HandLayout.enabled = false;
    }
    private void Start()
    {
        // Creates starting deck
        for(int i = 0; i < PossibleCards.Count; i++)
        {
            for(int y = 0; y < Instances[i]; y++)
            {
                GameObject temp = PossibleCards[i];
                Instantiate(temp,placement.position,placement.rotation, DiscardPile.transform);
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

    public void FixedUpdate()
    {
        if(HandLayout.enabled == true)
        {
            ToggleHandLayout(false);
        }
    }

    public void Shuffle()
    {
        for (int i = DiscardPile.transform.childCount; i > 0; i--)
        {
            Debug.Log("Shuffled");
            int randNumber = Random.Range(0, i);
            if(DiscardPile.transform.childCount > 0)
            {
                MoveCard(0, DiscardPile.transform.GetChild(randNumber).gameObject);
            }
        }
    }

    public void Draw()
    {
        ToggleHandLayout(true);
        if (HandPile.transform.childCount < 6)
        {
            if (DeckPile.transform.childCount == 0)
            {
                Shuffle();
            }
            Debug.Log("Popped");
            MoveCard(2, DeckPile.transform.GetChild(0).gameObject);
        }
    }

    public void MoveCard(int position, GameObject card)
    {
        ToggleHandLayout(true);
        if (position == 0)
        {
            Debug.Log("put in Deck");
            card.transform.SetParent(DeckPile.transform, false);
        }
        if (position == 1)
        {
            Debug.Log("put in Discard");
            card.transform.SetParent(DiscardPile.transform, false);
        }
        if (position == 2)
        {
            Debug.Log("put in Hand");
            card.transform.SetParent(HandPile.transform, false);
        }
    }

    public void ToggleHandLayout(bool setting)
    {
        HandLayout.enabled = setting;
        Debug.Log("Hand Layout Set to " + setting);
    }
}
