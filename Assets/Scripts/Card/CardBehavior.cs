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
            //targets = FindObjectsOfType<Enemy>().gameObject
        }
        else if (card.target == TargetingOption.Player)
        {
            //targets.Add(Player.instance.gameObject)
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
        //player.instance.GainShield(shieldAmount)
    }

    protected void DrawCards(int amt)
    {
        //deck.DrawCards(amt)
    }

    protected void SpendGrit(int cost)
    {
        //player.instance.LoseGrit() (could also be GainGrit with a negative value
    }

    protected void GainGrit(int amt)
    {
        //player.instance.GainGrit()
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
