using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public Encounter encounter;
    public CombatController combat;

    public List<Encounter> encounterList;
    [SerializeField] GameObject enemyPanel;
    [SerializeField] Transform[] spawnPoints = new Transform[3];

    bool encounterMade = false;

    private void Awake()
    {
        //should call a higher powerlevel every few combats
        GetEncounter(0);
        encounter.SetUp();
        DeployEncounter();
        encounterMade = true;
    }

    public void Update() {
        if(combat.state == CombatState.RewardScreen)
        {
            encounterMade = false;
        }

        if (combat.state == CombatState.PlayerTurn && combat.priorState == CombatState.RemoveCard && !encounterMade) {
            GetEncounter(0);
            encounter.SetUp();
            DeployEncounter();
            encounterMade = true;
        }
    }
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
                GameObject inst = Instantiate(encounter.enemyPrefab.gameObject, spawnPoints[i].position, Quaternion.identity, enemyPanel.transform);
                EnemyDisplay display = inst.GetComponent<EnemyDisplay>();
                display.enemy = encounter.encounterData[i];
                inst.transform.localScale = new Vector3(54f,54f,54f);
            }
        }
    }
}
