using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDeck : MonoBehaviour
{
    public GameObject deckPanel;
    public GameObject[] cardsInDeckPanel;
    public GameObject[] viewedCards;
    public Transform[] cardPlacements;
    public float cardScaleX;
    public float cardScaleY;
    public float cardScaleZ;

    // Start is called before the first frame update
    void OnEnable()
    {
        UpdateDeck();
    }

    private void OnDisable()
    {
        for (int i = 0; i < viewedCards.Length; i++)
        {
            Destroy(viewedCards[i].gameObject);

            viewedCards[i] = null;
            cardsInDeckPanel[i] = null;
        }
    }

    void UpdateDeck()
    {
        // create array of cards that are in the deck currently
        cardsInDeckPanel = new GameObject[deckPanel.transform.childCount];

        // create array of cards to be displayed in view deck screen
        viewedCards = new GameObject[deckPanel.transform.childCount];

        // show cards in deck
        for (int i = 0; i < cardsInDeckPanel.Length; i++)
        {
            // get cards that are childrened to the deck panel
            cardsInDeckPanel[i] = deckPanel.transform.GetChild(i).gameObject;

            // create card and set its parent to an empty object in the card layout panel
            viewedCards[i] = Instantiate(cardsInDeckPanel[i]);
            viewedCards[i].transform.SetParent(cardPlacements[i]);

            // set card's position and scale
            viewedCards[i].transform.localPosition = Vector3.zero;
            viewedCards[i].transform.localScale = new Vector3(cardScaleX, cardScaleY, cardScaleZ);
        }
    }
}
