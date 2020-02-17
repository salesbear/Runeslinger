using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounter", menuName = "Encounter")]
public class Encounter : ScriptableObject
{
    
    [SerializeField] public int powerLevel;

    [Tooltip("Do NOT use multiple of the Same Enemy, things will break")]
    [SerializeField] public Enemy enemy1;
    [SerializeField] public Enemy enemy2;
    [SerializeField] public Enemy enemy3;
    [SerializeField] public EnemyDisplay enemyPrefab;

    public Enemy[] encounterData;

    public void SetUp()
    {
        encounterData =  new Enemy[] { enemy1, enemy2, enemy3 };
    }
}
