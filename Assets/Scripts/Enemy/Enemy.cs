using System;
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

//other status effects can be added as needed
public enum StatusEffects
{
    Accuracy,
    Burn
}

public struct Statuses
{
    StatusEffects status;
    int effectPower;
    int numberOfTurns;
}


[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject, IDamagable
{
    

    //TODO: Actions to alert EnemyDisplay that the Enemy's Stats have changed
    public event Action EnemyUIUpdate = delegate { };
    public event Action Die = delegate { };
    public event Action<int, Color> DamageNumber = delegate { };

    public string enemyName;
    public int maxHP;
    public int currentHP; //should usually be equal to maxHP
    public int startingShield; //usually is 0
    [HideInInspector] public int currentShield;

    public int hiRollDmg;
    public int loRollDmg;
    public int rolledDamage { get; private set; } //the damage number that will appear on the icon thingy

    [Tooltip("How much shield the enemy can give itself")]
    public int shielding; //how much shield the enemy can give itself

    public PreparedAction preparedAction { get; private set; } 
    public Sprite enemySprite;

    public int startingStep; //usually should be 0
    [HideInInspector] public int behaviorStep;    
    public PreparedAction[] behavior = new PreparedAction[5];

    

    public void SetUp()
    {
        currentHP = maxHP;
        currentShield = startingShield;
        behaviorStep = startingStep;
        preparedAction = behavior[behaviorStep];
        if (preparedAction == PreparedAction.Attack)
        {
            RollDamage();
        }
        EnemyUIUpdate?.Invoke();
    }

    public void StepBehavior()
    {
        //check to make sure shit hasn't just gone completely south
        if (behaviorStep + 1 >= behavior.Length || behaviorStep < 0)
        {
            //if it has, reset
            behaviorStep = 0;
        }
        else
        {
            //otherwise, business as normal.
            behaviorStep++;
            //loop behavior
            if (behaviorStep > behavior.Length)
            {
                behaviorStep = 0;
            }
        }
        preparedAction = behavior[behaviorStep];
        if(preparedAction == PreparedAction.Attack)
        {
            RollDamage();
        }
        EnemyUIUpdate?.Invoke();
    }

    public void RollDamage()
    {
        rolledDamage = UnityEngine.Random.Range(loRollDmg, hiRollDmg);
    }

    //public void DealDamage(Player target)
    public void DealDamage(Enemy target) //overload for possible silly bullshit
    {
        target.TakeDamage(rolledDamage);
    }

    //overloaded method to deal with players
    public void DealDamage(IDamagable target)
    {
        target.TakeDamage(rolledDamage);
    }

    public void TakeDamage(int damageTaken)
    {
        if (currentShield > 0)
        {
            int shld = currentShield;
            currentShield -= damageTaken;
            if (currentShield < 0)
            {
                int carryoverDamage = currentShield;
                currentHP += carryoverDamage; //any negative shield is carried over as HP damage
                DamageNumber?.Invoke(shld, DmgNumbers.shieldDamageColor);
                if (carryoverDamage != 0)
                {
                    DamageNumber?.Invoke(carryoverDamage, DmgNumbers.damageColor);
                }
                currentShield = 0;
            }
            else
            {
                DamageNumber?.Invoke(damageTaken, DmgNumbers.shieldDamageColor);
            }
        }
        else
        {
            currentHP -= damageTaken;
            DamageNumber?.Invoke(damageTaken, DmgNumbers.damageColor);
        }

        if (currentHP <= 0)
        {
            HighScore highScore = FindObjectOfType<HighScore>();
            highScore.IncreaseHighScore();
            Die?.Invoke();
        }
        EnemyUIUpdate?.Invoke();
    }

    public void Shield(int shieldAmount)
    {
        currentShield += shieldAmount;
        EnemyUIUpdate?.Invoke();
    }
    public void Shield(Enemy target, int shieldAmount) //overload for shielding another enemy
    {
        target.currentShield += shieldAmount;
        EnemyUIUpdate?.Invoke();
    }
    
    public void RemoveShield()
    {
        currentShield = 0;
    }
}
