using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardDisplay))]
public class CardBehavior : MonoBehaviour
{
    //the card we're playing
    private Card card;
    private List<GameObject> targets;
    
    private void Awake()
    {
        //get the card we set in card display
        card = GetComponent<CardDisplay>().card;
        if (card.target == TargetingOption.Enemies)
        {
            targets = new List<GameObject>();
            EnemyDisplay[] enemies = FindObjectsOfType<EnemyDisplay>();
            foreach (EnemyDisplay enemyDisplay in enemies)
            {
                targets.Add(enemyDisplay.gameObject);
            }
        }
        else if (card.target == TargetingOption.Player)
        {
            targets.Add(PlayerStats.instance.gameObject);
        }
    }

    protected void DealDamage(int damage, GameObject[] targets)
    {
        foreach (GameObject target in targets)
        {
            IDamagable targetDamagable = target.GetComponent<IDamagable>();
            targetDamagable.TakeDamage(damage);
        }
    }

    protected void Shield(int shieldAmount)
    {
        PlayerStats.instance.GainShieldForXTurns(PlayerStats.instance.posStatus, shieldAmount, 1);
    }

    protected void DrawCards(int amt)
    {
        //deck.DrawCards(amt)
    }

    protected void SpendGrit(int cost)
    {
        PlayerStats.instance.GainGritForXTurns(PlayerStats.instance.posStatus, -cost, 1);
    }

    protected void GainGrit(int amt)
    {
        PlayerStats.instance.GainGritForXTurns(PlayerStats.instance.posStatus, amt, 1);
    }

    public void PlayCard()
    {
        DealDamage(card.damage,targets.ToArray());
        Shield(card.shield);
        SpendGrit(card.gritCost);
        GainGrit(card.gritGained);
        //deck.DiscardCard(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        targets.Clear();
        targets.Add(target);
    }

    public bool HasTarget()
    {
        return (targets != null);
    }
}
