using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public BaseClassless playerClass;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = new BaseClassless();
    }
}
