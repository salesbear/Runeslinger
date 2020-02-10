using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PreparedAction
{
    DoNothing = 0,
    Attack,
    Defend,
    Buff,
    Debuff,
    Other
}

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    //TODO: Actions to alert EnemyDisplay that the Enemy's Stats have changed

    public string enemyName;
    public int maxHP;
    public int currentHP; //should usually be equal to maxHP
    public int currentShield; //should usually be 0

    public int hiRollDmg;
    public int loRollDmg;
    public int rolledDamage { get; private set; } //the damage number that will appear on the icon thingy

    public int shielding; //how much shield the enemy can give itself

    public PreparedAction preparedAction { get; private set; } 
    public Sprite enemySprite;
    
    public int behaviorStep;    //usually should be 0
    public PreparedAction[] behavior = new PreparedAction[5];

    public void SetUp()
    {
        preparedAction = behavior[behaviorStep];
        if (preparedAction == PreparedAction.Attack)
        {
            RollDamage();
        }
    }

    public void stepBehavior()
    {
        //check to make sure shit hasn't just gone completely south
        if(behaviorStep > behavior.Length || behaviorStep < 0)
        {
            //if it has, reset
            behaviorStep = 0;
            return;
        }

        //otherwise, business as normal.
        behaviorStep++;
        //loop behavior
        if(behaviorStep > behavior.Length)
        {
            behaviorStep = 0;
        }

        preparedAction = behavior[behaviorStep];

        if(preparedAction == PreparedAction.Attack)
        {
            RollDamage();
        }
    }

    public void RollDamage()
    {
        rolledDamage = Random.Range(loRollDmg, hiRollDmg);
    }

    //public void DealDamage(Player target)
    public void DealDamage(Enemy target) //overload for possible silly bullshit
    {
        target.TakeDamage(rolledDamage);
    }


    public void TakeDamage(int damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHP += currentShield; //any negative shield is carried over as HP damage
            }
        }
        else
        {
            currentHP -= damage;
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Shield(int shieldAmount)
    {
        currentShield += shieldAmount;
    }
    public void Shield(Enemy target, int shieldAmount) //overload for shielding another enemy
    {
        target.currentShield += shieldAmount;
    }
    

    public void Die()
    {
        GameObject.Destroy(this);
    }

}
