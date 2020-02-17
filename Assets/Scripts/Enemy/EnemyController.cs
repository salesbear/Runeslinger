using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    //all the enemies in the scene
    [ReadOnly]
    public List<EnemyDisplay> enemies = new List<EnemyDisplay>();
    
    //event to tell combat controller when enemies are done with their turn or dead
    public static event Action<int> EnemyState = delegate { };

    private void OnEnable()
    {
        CombatController.StateChanged += OnEnemyTurn;
    }

    private void OnDisable()
    {
        CombatController.StateChanged -= OnEnemyTurn;
    }

    public void AddEnemy(EnemyDisplay enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(EnemyDisplay enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count <= 0)
        {
            //tell the combat controller that the player killed all the enemies
            EnemyState.Invoke(6);
        }
    }

    //do all of the enemy actions, then shift to discard state
    public void OnEnemyTurn(CombatState state)
    {
        if (state == CombatState.EnemyTurn)
        {
            
            foreach (EnemyDisplay enemyDisplay in enemies)
            {
                //clear any remaining enemy shield
                enemyDisplay.enemy.RemoveShield();

                //then do all the enemy actions
                PreparedAction action = enemyDisplay.enemy.preparedAction;
                if (action == PreparedAction.Attack)
                {
                    enemyDisplay.enemy.DealDamage(PlayerStats.instance);
                }
                else if (action == PreparedAction.Defend)
                {
                    enemyDisplay.enemy.Shield(enemyDisplay.enemy.shielding);
                }
                else
                {
                    Debug.Log("Not Implemented or doing nothing");
                }
                //go to next action in the pool
                enemyDisplay.enemy.StepBehavior();
                
            }
            //go to discard state
            EnemyState.Invoke(3);
        }
    }
}
