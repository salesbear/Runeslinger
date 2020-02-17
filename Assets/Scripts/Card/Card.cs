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
    Classless,
    Fire,
    Water, 
    Earth,
    Air
}

public enum CardRarity
{
    Starter = 0,
    Common,
    Uncommon,
    Rare,
    Debuff
}
public enum CardType
{
    Ailment, // negative effect
    Attack,
    Action, // one-off effect
    Ability // positive effect
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public CardClass cardClass;
    public CardType cardType;
    public CardRarity cardRarity;
    [TextArea(2, 3)]
    public string description;
    public Sprite cardImage;
    public int gritCost;
    [Tooltip("The cards you draw on play")]
    public int cardsDrawn = 0 ;
    [Tooltip("The amount of damage you do, negative for healing")]
    public int damage = 0;
    [Tooltip("The amount you shield the player")]
    public int shield = 0;
    [Tooltip("The amount of grit you gain")]
    public int gritGained = 0;

    public TargetingOption target;
}
