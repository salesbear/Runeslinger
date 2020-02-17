using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDeck : MonoBehaviour
{
    public GameObject deckPanel;
    GameObject[] cardsInDeck;
    public Transform[] cardPlacements;
    public float cardScaleX;
    public float cardScaleY;
    public float cardScaleZ;

    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("UpdateDeck", 0.5f);
    }

    private void OnDisable()
    {
       for (int i = 0; i < cardsInDeck.Length; i++)
       {
            Destroy(cardPlacements[i].GetChild(0));
       }
    }

    void UpdateDeck()
    {
        cardsInDeck = new GameObject[deckPanel.transform.childCount];
        
        for (int i = 0; i < cardsInDeck.Length; i++)
        {
            cardsInDeck[i] = deckPanel.transform.GetChild(i).gameObject;

            // create card and set its parent to an empty object in the card layout panel
            GameObject cardPos = Instantiate(cardsInDeck[i]);
            cardPos.transform.SetParent(cardPlacements[i]);

            // set card's position and scale
            cardPos.transform.localPosition = cardPlacements[i].position;
            cardPos.transform.localScale = new Vector3(cardScaleX, cardScaleY, cardScaleZ);
        }
    }
}
