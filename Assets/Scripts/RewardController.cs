using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RewardController : MonoBehaviour
{
    [Header("Controls")]
    [Tooltip("The points where cards can spawn, should only add transforms to this")]
    [SerializeField]
    Placement[] rewardSpawns;
    [SerializeField]
    Placement[] removeCardSpawns;
    [Tooltip("The percent chance of getting a common")]
    [SerializeField] int commonPercentage;
    [Tooltip("The percent chance of getting an uncommon")]
    [SerializeField] int uncommonPercentage;
    //rare percentage = 100 - commonPercentage - uncommonPercentage

    [Tooltip("The max number of rounds the player can go without getting an uncommon")]
    [SerializeField]
    int uncommonLimit = 2;
    [Tooltip("The max number of rounds the player can go without getting a rare")]
    [SerializeField]
    int rareLimit = 4;

    //used to track if pity timers should increase after card generation
    bool spawnedRare = false;
    bool spawnedUncommon = false;
    //used to add card to deck after player chooses it
    Stack theStack;

    [Header("Debugging")]
    //these aren't here solely for debugging, but they're visible because debugging
    [ReadOnly]
    public GameObject rewardChosen;
    [ReadOnly]
    public GameObject cardToRemove;

    GameObject[] deck;

    private void Awake()
    {
        theStack = FindObjectOfType<Stack>();
    }

    private void Start()
    {
        if (PlayerStats.instance != null)
        {
            deck = PlayerStats.instance.playerClass.deckList;
        }
    }

    private void OnEnable()
    {
        CombatController.StateChanged += GenerateReward;
    }

    private void OnDisable()
    {
        CombatController.StateChanged -= GenerateReward;
    }


    void GenerateReward(CombatState state)
    {
        if (state == CombatState.RewardScreen)
        {
            //CardGenerator.instance.LoadCards();
            for (int i = 0; i < rewardSpawns.Length; i++)
            {
                //if it's been 4 or more rounds since we saw a rare
                if (PlayerStats.instance.rarePityTimer >= rareLimit)
                {
                    //generate a rare card and put it in our list of rewards
                    GameObject card = CardGenerator.instance.GenerateCard(CardRarity.Rare,true);
                    GameObject temp = Instantiate(card);
                    rewardSpawns[i].card = temp;
                    rewardSpawns[i].hasCard = true;
                    //instantiate card
                    Instantiate(card, rewardSpawns[i].point);
                    //reset pity timer
                    PlayerStats.instance.rarePityTimer = 0;
                    spawnedRare = true;
                }
                //if it's been two or more rounds since we saw an uncommon
                else if (PlayerStats.instance.uncommonPityTimer >= uncommonLimit)
                {
                    //Generate an uncommon card and put it in our list of rewards
                    GameObject card = CardGenerator.instance.GenerateCard(CardRarity.Uncommon, true);
                    GameObject temp = Instantiate(card,rewardSpawns[i].point);
                    rewardSpawns[i].card = temp;
                    rewardSpawns[i].hasCard = true;
                    //reset pity timer
                    PlayerStats.instance.uncommonPityTimer = 0;
                    spawnedUncommon = true;
                }
                else
                {
                    //get a number between 1 and 100
                    int rand = Random.Range(1, 101);
                    if (rand < commonPercentage)
                    {
                        //Generate an uncommon card and put it in our list of rewards
                        GameObject card = CardGenerator.instance.GenerateCard(CardRarity.Common, true);
                        GameObject temp = Instantiate(card,rewardSpawns[i].point);
                        rewardSpawns[i].card = temp;
                        rewardSpawns[i].hasCard = true;
                    }
                    else if (rand > commonPercentage && rand < commonPercentage + uncommonPercentage)
                    {
                        //Generate an uncommon card and put it in our list of rewards
                        GameObject card = CardGenerator.instance.GenerateCard(CardRarity.Uncommon, true);
                        GameObject temp = Instantiate(card,rewardSpawns[i].point);
                        rewardSpawns[i].card = temp;
                        rewardSpawns[i].hasCard = true;
                        //reset pity timer
                        PlayerStats.instance.uncommonPityTimer = 0;
                        spawnedUncommon = true;
                    }
                    else
                    {
                        //generate a rare card and put it in our list of rewards
                        GameObject card = CardGenerator.instance.GenerateCard(CardRarity.Rare, true);
                        GameObject temp = Instantiate(card,rewardSpawns[i].point);
                        rewardSpawns[i].card = temp;
                        rewardSpawns[i].hasCard = true;
                        //reset pity timer
                        PlayerStats.instance.rarePityTimer = 0;
                        spawnedRare = true;
                    }
                }
            }
            //update pity timers
            if (!spawnedRare)
            {
                PlayerStats.instance.rarePityTimer++;
            }
            if (!spawnedUncommon)
            {
                PlayerStats.instance.uncommonPityTimer++;
            }
        }
    }
    /// <summary>
    /// delete all the cards that we used to select the reward
    /// </summary>
    public void DeleteCards()
    {
        GameObject temp = CardGenerator.instance.GetCardByName(rewardChosen.GetComponent<CardDisplay>().card.ToString());
        for(int i = 0; i < rewardSpawns.Length; i++)
        {
            GameObject temp2 = rewardSpawns[i].card;
            rewardSpawns[i].card = null;
            Destroy(temp2);
            rewardSpawns[i].hasCard = false;
        }
        //get the prefab of the card we want to put in player's deck
        rewardChosen = temp;
    }
    /// <summary>
    /// add the new card to the deck and remove the old one
    /// </summary>
    public void ReplaceCard()
    {
        //CardGenerator.instance.LoadCards();
        for (int i = 0; i < PlayerStats.instance.playerClass.deckList.Length; i++)
        {
            //find the card information from player stats
            Card temp = cardToRemove.GetComponent<CardDisplay>().card;
            //haha, this sucks man (i'm checking to see where the card we're replacing is)
            if (removeCardSpawns[i].card.GetComponent<CardDisplay>().card.ToString() == temp.ToString())
            {
                //replace the card in the deck list and destroy the one we're removing
                PlayerStats.instance.playerClass.deckList[i] = rewardChosen;
                //theStack.AddCard(rewardChosen);
                DeletePlayerDeck();
                break;
            }
        }
        //going to a new round, so reset everything
        //theStack.RemoveCardFromDeck(cardToRemove.GetComponent<CardDisplay>().card.ToString());
        Destroy(cardToRemove.gameObject);
        cardToRemove = null;
        rewardChosen = null;
        SaveController.SaveGame();
        //TODO: put in animation/delay for removing card
        //reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowPlayerDeck()
    {
        DeletePlayerDeck();
        for (int i = 0; i < deck.Length; i++)
        {
            GameObject card = Instantiate(deck[i], removeCardSpawns[i].point);
            removeCardSpawns[i].card = card;
            removeCardSpawns[i].hasCard = true;
        }
    }

    public void DeletePlayerDeck()
    {
        for (int i = 0; i < removeCardSpawns.Length; i++)
        {
            if (removeCardSpawns[i].hasCard)
            {
                GameObject temp = removeCardSpawns[i].card;
                removeCardSpawns[i].card = null;
                removeCardSpawns[i].hasCard = false;
                Destroy(temp);
            }
        }
    }
}
