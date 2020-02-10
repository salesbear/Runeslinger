using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    BaseClassless playerClass = new BaseClassless();

    // Start is called before the first frame update
    void Start()
    {
        playerClass.Classless();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerClass.maxHealth);
    }
}
