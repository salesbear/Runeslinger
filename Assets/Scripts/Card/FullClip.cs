using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullClip : CardBehavior
{
    public override void PlayCard()
    {
        if (card.gritCost <= PlayerStats.instance.playerClass.currentGrit)
        {
            DealDamage(card.damage, targets.ToArray());
            Shield(card.shield);
            SpendGrit(card.gritCost);
            GainGrit(card.gritGained);
            if (card.cardType == CardType.Ability)
            {
                deck.ExileCard(gameObject);
            }
            else
            {
                deck.DiscardCard(gameObject);
            }
            DrawCards(card.cardsDrawn);
            deck.DiscardHand();
        }
        else
        {
            deck.ResetHand();
        }
    }
}
