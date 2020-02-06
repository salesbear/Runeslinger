using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardBehavior : MonoBehaviour
{
    //the card we're playing
    private Card card;
    private List<GameObject> targets;
    private void Awake()
    {
        card = GetComponent<Card>();
        if (card.target == TargetingOption.Enemies)
        {
            //targets = FindObjectsOfType<Enemy>()
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
    }

    public void SetTarget(GameObject target)
    {
        targets.Clear();
        targets.Add(target);
    }
}
