using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardGenerator : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    GameObject[] cards;
    public static CardGenerator instance;

    //singleton design pattern
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cards = Resources.LoadAll<GameObject>("CardPrefabs");
    }

    /// <summary>
    /// Generates a random card from a specific rarity, or any non-starter card if left empty
    /// </summary>
    /// <param name="targetRarity">the rarity of cards you want to generate</param>
    /// <param name="filterRarity">set to true to generate only from specified rarity</param>
    /// <returns></returns>
    public GameObject GenerateCard(CardRarity targetRarity = CardRarity.Debuff, bool filterRarity = false)
    {
        int rand;
        //temp list to hold cards
        List<GameObject> temp = new List<GameObject>();
        for (int i = 0; i < cards.Length; i++)
        {
            if (filterRarity)
            {
                if (cards[i].GetComponent<CardDisplay>().card.cardRarity == targetRarity)
                {
                    temp.Add(cards[i]);
                }
            }
            else
            {
                if (cards[i].GetComponent<CardDisplay>().card.cardRarity != CardRarity.Starter)
                {
                    temp.Add(cards[i]);
                }
            }
        }
        rand = Random.Range(0, temp.Count);
        return temp[rand];
    }

    /// <summary>
    /// Get an array list containing the starter cards
    /// </summary>
    /// <returns></returns>
    public ArrayList GetStarters()
    {
        ArrayList starterList = new ArrayList();
        foreach (GameObject card in cards)
        {
            if (card.GetComponent<CardDisplay>().card.cardRarity == CardRarity.Starter)
            {
                starterList.Add(card);
            }
        }
        return starterList;
    }
}
