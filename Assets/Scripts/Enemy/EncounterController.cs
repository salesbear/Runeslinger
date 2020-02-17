using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public Encounter encounter;

    public List<Encounter> encounterList;

    [SerializeField] Transform[] spawnPoints = new Transform[3];

    public void GetEncounter(int level)
    {
        List<Encounter> possibleEncounters = new List<Encounter> { };
        foreach (Encounter ncountr in encounterList)
        {
            if (ncountr.powerLevel == level)
            {
                possibleEncounters.Add(ncountr);
            }
        }
        if (possibleEncounters.Count > 0) { 
            int encounterNumber = Random.Range(0, possibleEncounters.Count);
            encounter = possibleEncounters[encounterNumber];
        }
    }

    public void DeployEncounter()
    {
        for (int i = 0; i < encounter.encounterData.Length; i++)
        {
            if (encounter.encounterData[i] != null)
            {
                Instantiate(encounter.enemyPrefab.gameObject, spawnPoints[i]);
                encounter.enemyPrefab.enemy = encounter.encounterData[i];
            }
        }
    }
}
