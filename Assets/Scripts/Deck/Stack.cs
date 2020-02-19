using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//used to convert from array to list, apparently this is a slow thing to do so maybe we should refactor some stuff
using System.Linq;

[System.Serializable]
public struct Placement
{
    public Transform point;
    public bool hasCard;
    public GameObject card;
}

public class Stack : MonoBehaviour
{
    //[System.Serializable]
    //public struct Placement
    //{
    //    public Transform point;
    //    public bool hasCard;
    //    public GameObject card;
    //}

    [SerializeField] List<GameObject> PossibleCards = new List<GameObject>();
    //[SerializeField] List<int> Instances = new List<int>();
    [SerializeField] Transform[] CardSpawnPoints;
    [SerializeField] GameObject DeckPile;
    [SerializeField] GameObject DiscardPile;
    [SerializeField] GameObject HandPile;
    [SerializeField] GameObject ExilePile;
    [SerializeField] Transform placement;

    private Placement[] cardPlacements = new Placement[6];
    //idk why this isn't working, ugh
    private HorizontalLayoutGroup HandLayout;

    private void Awake()
    {
        // Get the hand layout
        HandLayout = GetComponentInChildren<HorizontalLayoutGroup>();
        HandLayout.enabled = false;
    }
    private void Start()
    {
        //get our possible cards from our player's deck
        PossibleCards = PlayerStats.instance.playerClass.deckList.OfType<GameObject>().ToList();
        // Creates starting deck
        for (int i = 0; i < PossibleCards.Count; i++)
        {
            //for(int y = 0; y < Instances[i]; y++)
            //{
            GameObject temp = PossibleCards[i];
            Instantiate(temp,placement.position,placement.rotation, DiscardPile.transform);
            //}
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
        //initialize card placements
        for (int i = 0; i < cardPlacements.Length; i++)
        {
            cardPlacements[i].point = CardSpawnPoints[i];
            cardPlacements[i].hasCard = false;
        }
    }

    //public void FixedUpdate()
    //{
    //    if(HandLayout.enabled == true)
    //    {
    //        ToggleHandLayout(false);
    //    }
    //}

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
        //ToggleHandLayout(true);
        if (GetOpenSpot() > -1)
        {
            if (DeckPile.transform.childCount == 0)
            {
                Shuffle();
            }
            if (DeckPile.transform.childCount != 0)
            {
                Debug.Log("Popped");
                MoveCard(2, DeckPile.transform.GetChild(0).gameObject);
            }
        }
    }

    /// <summary>
    /// moves a card to specified location
    /// </summary>
    /// <param name="position">where to move card, 0 = Deck, 1 = Discard, 2 = hand, 3 = Exile</param>
    /// <param name="card"></param>
    public void MoveCard(int position, GameObject card)
    {
        //ToggleHandLayout(true);
        if (position == 0)
        {
            //if the card's in hand remove it
            int cardSpot = GetCardSpot(card);
            if (cardSpot > -1)
            {
                cardPlacements[cardSpot].hasCard = false;
                cardPlacements[cardSpot].card = null;
            }
            Debug.Log("put in Deck");
            card.transform.SetParent(DeckPile.transform, false);
        }
        if (position == 1)
        {
            //if the card's in hand remove it
            int cardSpot = GetCardSpot(card);
            if (cardSpot > -1)
            {
                cardPlacements[cardSpot].hasCard = false;
                cardPlacements[cardSpot].card = null;
            }
            Debug.Log("put in Discard");
            card.transform.SetParent(DiscardPile.transform, false);
        }
        if (position == 2)
        {
            Debug.Log("put in Hand");
            card.transform.SetParent(HandPile.transform);
            int openSpot = GetOpenSpot();
            if (openSpot != -1)
            {
                card.transform.position = cardPlacements[openSpot].point.position;
                cardPlacements[openSpot].hasCard = true;
                cardPlacements[openSpot].card = card;
            }
        }
        if (position == 3)
        {
            int cardSpot = GetCardSpot(card);
            if (cardSpot > -1)
            {
                cardPlacements[cardSpot].hasCard = false;
                cardPlacements[cardSpot].card = null;
            }
            Debug.Log("put in Exile");
            card.transform.SetParent(ExilePile.transform, false);
        }
    }

    public void ToggleHandLayout(bool setting)
    {
        HandLayout.enabled = setting;
        Debug.Log("Hand Layout Set to " + setting);
    }

    /// <summary>
    /// Discard all the remaining cards in your hand
    /// </summary>
    public void DiscardHand()
    {
        for (int i = 0; i < cardPlacements.Length; i++)
        {
            if (cardPlacements[i].hasCard)
            {
                MoveCard(1, cardPlacements[i].card);
            }
        }
    }

    public void DiscardCard(GameObject card)
    {
        MoveCard(1, card);
    }

    public void ExileCard(GameObject card)
    {
        MoveCard(3, card);
    }
    /// <summary>
    /// draw amt cards
    /// </summary>
    /// <param name="amt"></param>
    public void DrawCards(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            Draw();
        }
    }

    //returns the index of the first open spot or -1 if all are taken
    private int GetOpenSpot()
    {
        for (int i = 0; i < cardPlacements.Length; i++)
        {
            if (!cardPlacements[i].hasCard)
            {
                return i;
            }
        }
        return -1;
    }
    //returns the index where the card is in hand, or -1 if it's not in hand
    private int GetCardSpot(GameObject card)
    {
        for (int i = 0; i < cardPlacements.Length; i++)
        {
            if (cardPlacements[i].card == card)
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// resets a specific card's position
    /// </summary>
    /// <param name="card"></param>
    public void ResetCard(GameObject card)
    {
        int cardIndex = GetCardSpot(card);
        if (cardIndex > -1)
        {
            card.transform.position = cardPlacements[cardIndex].point.position;
        }
    }
    /// <summary>
    /// resets positions for all cards in hand
    /// </summary>
    public void ResetHand()
    {
        foreach (Placement placement in cardPlacements)
        {
            if (placement.hasCard)
            {
                placement.card.transform.position = placement.point.position;
            }
        }
    }
    /// <summary>
    /// move every card in the deck to discard, renewing the deck for the next round
    /// </summary>
    public void RenewDeck()
    {
        DiscardHand();
        while (ExilePile.transform.childCount > 0)
        {
            MoveCard(1, ExilePile.transform.GetChild(0).gameObject);
        }
        while (DeckPile.transform.childCount > 0)
        {
            MoveCard(1, DeckPile.transform.GetChild(0).gameObject);
        }
    }

    public void viewWholeDeck()
    {

    }
}
