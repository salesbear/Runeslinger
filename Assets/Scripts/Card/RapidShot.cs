using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidShot : CardBehavior
{
    protected override void DealDamage(int damage, GameObject[] targets)
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (GameObject target in targets)
            {
                //need to check so the game doesn't crash when your first shot kills the enemy
                if (target!= null)
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
                        if (targetDamagable != null)
                        {
                            targetDamagable.TakeDamage(damage + PlayerStats.instance.playerClass.accuracy);
                        }
                        
                    }
                    else
                    {
                        targetDamagable.TakeDamage(damage);
                    }
                }

            }
        }
    }
}
