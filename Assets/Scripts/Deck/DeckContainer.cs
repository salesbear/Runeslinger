using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeckContainer : MonoBehaviour
{
    public GameObject[] deck = new GameObject[12];
    public GameObject[] StarterCards = new GameObject[2];
    public GameObject[] CommonCards = new GameObject[4];
    public GameObject[] UncommonCards = new GameObject[5];
    public GameObject[] RareCards = new GameObject[3];

    //This is currently set to activate when the New Game Button is pressed
    public void RandomizeStartingDeck()
    {
        int i = 0;
        for (i = i; i < 4; i++)
        {
            deck[i] = StarterCards[0];
        }
        for (i = i; i < 8; i++)
        {
            deck[i] = StarterCards[1];
        }
        for (i = i; i < 11; i++)
        {
            deck[i] = CommonCards[UnityEngine.Random.Range(0, CommonCards.Length)];
        }
        deck[i] = UncommonCards[UnityEngine.Random.Range(0, UncommonCards.Length)];
    }
}
