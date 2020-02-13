using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI grit;
    [SerializeField] TextMeshProUGUI accuracy;
    [SerializeField] TextMeshProUGUI shield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + PlayerStats.instance.playerClass.currentHealth;
        grit.text = "Grit: " + PlayerStats.instance.playerClass.currentGrit;
        accuracy.text = "Acc: " + PlayerStats.instance.playerClass.accuracy;
        shield.text = "Shield: " + PlayerStats.instance.playerClass.shield;
    }
}
