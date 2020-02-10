using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCheck : MonoBehaviour
{
    [SerializeField] Enemy _attackingFucker = null;
    [SerializeField] Enemy _targetFucker = null;

    List<Enemy> enemies = new List<Enemy>();
    void Start()
    {
        enemies.Add(_attackingFucker);
        enemies.Add(_targetFucker);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _attackingFucker.DealDamage(_targetFucker);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach( Enemy enemy in enemies )
            {
                enemy.StepBehavior();
            }
        }
    }
}
