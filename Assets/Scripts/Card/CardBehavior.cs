using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardDisplay))]
public class CardBehavior : MonoBehaviour
{
    //the card we're playing
    protected Card card;
    protected List<GameObject> targets = new List<GameObject>();
    protected Stack deck;

    private void Awake()
    {
        //get the card we set in card display
        card = GetComponent<CardDisplay>().card;
        //if we're targeting all enemies, find all enemies and add them to targets
        if (card.target == TargetingOption.Enemies)
        {
            EnemyDisplay[] enemies = FindObjectsOfType<EnemyDisplay>();
            foreach (EnemyDisplay enemyDisplay in enemies)
            {
                targets.Add(enemyDisplay.gameObject);
            }
        }
        //else if we're targeting the player, add the player to our targets
        else if (card.target == TargetingOption.Player)
        {
            targets.Add(PlayerStats.instance.gameObject);
        }

        //get the deck
        deck = FindObjectOfType<Stack>();
    }

    protected virtual void DealDamage(int damage, GameObject[] targets)
    {
        foreach (GameObject target in targets)
        {
            //if there's a damageable component in the object, find it
            IDamagable targetDamagable = target.GetComponent<IDamagable>();
            if (targetDamagable == null)
            {
                targetDamagable = target.GetComponentInParent<IDamagable>();
            }
            if (targetDamagable == null)
            {
                targetDamagable = target.GetComponentInChildren<IDamagable>();
            }
            //only apply accuracy if target is an enemy
            if (card.target != TargetingOption.Player)
            {
                targetDamagable.TakeDamage(damage + PlayerStats.instance.playerClass.accuracy);
            }
            else
            {
                targetDamagable.TakeDamage(damage);
            }
        }
    }

    protected void Shield(int shieldAmount)
    {
        PlayerStats.instance.CallStatus(3, shieldAmount, 1);
    }

    protected void DrawCards(int amt)
    {
        deck.DrawCards(amt);
    }

    protected void SpendGrit(int cost)
    {
        PlayerStats.instance.CallStatus(1, -cost, 1);
    }

    protected void GainGrit(int amt)
    {
        PlayerStats.instance.CallStatus(1 , amt, 1);
    }

    public virtual void PlayCard()
    {
        if (card.gritCost <= PlayerStats.instance.playerClass.currentGrit)
        {
            DealDamage(card.damage,targets.ToArray());
            Shield(card.shield);
            SpendGrit(card.gritCost);
            GainGrit(card.gritGained);
            deck.DiscardCard(gameObject);
            DrawCards(card.cardsDrawn);
        }
        else
        {
            deck.ResetHand();
        }
    }

    /// <summary>
    /// sets target to be the game object in the parameter
    /// clears previous targets
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(GameObject target)
    {
        //make sure we don't erase our targets if it's enemies
        if (card.target == TargetingOption.Enemy)
        {
            targets.Clear();
            targets.Add(target);
        }
    }

    public bool HasTarget()
    {
        return (targets.Count >= 1);
    }
}
