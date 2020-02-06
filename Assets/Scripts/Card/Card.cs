using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TargetingOption
{
    None,
    Enemy,
    Enemies,
    Player,
}

public enum CardClass
{
    Fire,
    Water, 
    Earth,
    Air
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public CardClass cardClass;
    public string cardType;

    [TextArea(2, 3)]
    public string description;
    public Sprite cardImage;
    public int gritCost;

    public TargetingOption target;
}
