using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounter", menuName = "Encounter")]
public class Encounter : ScriptableObject
{
    
    public int powerLevel;

    [SerializeField] Enemy enemy1;
    [SerializeField] Enemy enemy2;
    [SerializeField] Enemy enemy3;
    public GameObject enemyPrefab;
    //enemy spawn points (?)

    Enemy[] encounterData;

    public void ActivateEncounter()
    {
        encounterData =  new Enemy[] { enemy1, enemy2, enemy3 };
        foreach (Enemy enemy in encounterData)
        {
            Debug.Log("Spawned Enemy");
            //Instantiate(enemyPrefab at enemy spawn point 1,2,3)
        }
    }
}
